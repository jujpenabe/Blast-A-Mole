using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using BAM.B;

namespace BAM.C
{
    public class PlanetController : MonoBehaviour
    {
        // Rotate planet
        [SerializeField] private float _rotationSpeed = 5f;
        // Planet's spots
        [SerializeField] private HoleInstantiator[] _spots = new HoleInstantiator[10]; //Spot = Hole
        // Moles model prefab
        [SerializeField] private GameObject _molePrefab;

        [SerializeField] private bool _gameOver = false;

        [SerializeField] private Transform _spawnReference;
        // Moles pool
        List<GameObject> _molesPool;

        public IReadOnlyReactiveProperty<int> Score => _score;
        private readonly ReactiveProperty<int> _score = new ReactiveProperty<int>(0);

        public IReadOnlyReactiveProperty<int> Lives => _lives;
        private readonly ReactiveProperty<int> _lives = new ReactiveProperty<int>(3);

        // Dependency injection
        private IScoreUsecase _usecase;
        
        public void Initialize(IScoreUsecase scoreUseCase)
        {
            _usecase = scoreUseCase;  // game Initialize

            var disposable = _usecase.Score.Subscribe((score) => _score.Value = score);
            scoreUseCase.Score.Subscribe(score => _score.Value = score);
        }
        private void Start()
        {
            _molesPool = FillPool(_spots.Length, _molePrefab);

            IObservable<Unit> update = this.UpdateAsObservable();

            update.Subscribe(_ => HandleRotation());
            // Subscribe to function to check if time is up
            update.Subscribe(_ => HandleTime());

            StartCoroutine(SpawnWave()); // Move to usecase
        }
        private List<GameObject> FillPool(int amount, GameObject prefab)
        {
            List<GameObject> pool = new List<GameObject>();
            GameObject tmp;
            for (int i = 0; i < amount; i++)
            {
                tmp = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                tmp.SetActive(false);
                pool.Add(tmp);
            }
            return pool;
        }
        private GameObject GetObjectFromPool(List<GameObject> pool)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (!pool[i].activeInHierarchy)
                {
                    return pool[i];
                }
            }
            return null;
        }
        private void HandleRotation()
        {   
            // Rotate planet in local Z axis
            transform.Rotate(new Vector3(0, 0, _rotationSpeed * Time.deltaTime));

            // transform.Rotate(new Vector3(_rotationSpeed * Time.deltaTime, 0, _rotationSpeed * Time.deltaTime));
        }
        
        private void HandleTime()
        {
            // If time is up, game over
            if (Time.timeSinceLevelLoad >= 120f)
            {
                _gameOver = true;
            }
        }
        private IEnumerator SpawnWave()
        {
            // Wait 3 seconds before starting the game
            yield return new WaitForSeconds(1f);
            // Game loop
            while (!_gameOver)
            {
                
                int molesToSpawn = UnityEngine.Random.Range(1 , 4 );

                // Get the first molesToSpawn element of _spots and save on a new temporary array
                // Increase range of spawnable holes, double the amount of moles to spawn
                int spawnRange = molesToSpawn * 2;
                var tempHoles = new HoleInstantiator[spawnRange];
                
                SortArrayByDistanceToPoint(_spots, _spawnReference.position);
                Array.Copy(_spots, tempHoles, spawnRange);

                for (int i = 0; i < molesToSpawn; i++)
                {
                    // get a random spot from the array and remove it
                    int randomSpot = UnityEngine.Random.Range(0, tempHoles.Length);

                    while (tempHoles[randomSpot].GetSpawnable() != null)
                    {
                        randomSpot = UnityEngine.Random.Range(0, tempHoles.Length);
                        yield return null;
                    }
                    
                    // Get mole from pool and set it to hole
                    GameObject mole = GetObjectFromPool(_molesPool);
                    // Wait if there are no moles available
                    while (mole == null)
                    {
                        yield return new WaitForSeconds(1f);
                        mole = GetObjectFromPool(_molesPool);
                    }
                    var tempHole = tempHoles[randomSpot];  // Refactor
                    tempHole.SetSpawnable(mole);
                    tempHole.LookAt(transform.position);
                    tempHole.Spawn();
                    
                    // Wait between 0.5 and 1.5 seconds
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
                }
                // Wait until all moles are inactive
                while (!AreAllObjectsInactive(_molesPool))
                {
                    
                    yield return new WaitForSeconds(1f);
                }
                ResetAllInstantiators(_spots);
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
            }
            yield return null;
        }
        private bool AreAllObjectsInactive(List<GameObject> pool)
        {
            for (int i = 0; i < pool.Count; i++)
            {
                if (pool[i].activeSelf)
                {
                    return false;
                }
            }
            return true;
        }
        private void ResetAllInstantiators(HoleInstantiator[] instantiators)
        {
            for (int i = 0; i < instantiators.Length; i++)
            {
                instantiators[i].SetSpawnable(null);
            }
        }
        private void SortArrayByDistanceToPoint(MonoBehaviour[] arr, Vector3 position)
        {
            Array.Sort(arr, (a, b) => Vector3.Distance(a.transform.position, position).CompareTo(Vector3.Distance(b.transform.position, position)));
        }
    }
}
