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
        [SerializeField] private GameObject[] _spots = new GameObject[10];
        // Moles to spawn on wave
        [SerializeField] private int _molesToPool;
        // Moles model prefab
        [SerializeField] private GameObject _molePrefab;
        // Moles pool
        public List<GameObject> _molesPool;

        public IReadOnlyReactiveProperty<int> Score => _score;
        private readonly ReactiveProperty<int> _score = new ReactiveProperty<int>(0);

        public IReadOnlyReactiveProperty<int> Lives => _lives;
        private readonly ReactiveProperty<int> _lives = new ReactiveProperty<int>(3);

        // Dependency injection
        private IScoreUsecase _usecase;
        
        public void Initialize(IScoreUsecase scoreUseCase)
        {
            _usecase = scoreUseCase;

            var disposable = _usecase.Score.Subscribe((score) => _score.Value = score);
            scoreUseCase.Score.Subscribe(score => _score.Value = score);
        }
        private void Start()
        {
            _molesToPool = _spots.Length;
            _molesPool = FillPool(_molesToPool, _molePrefab);

            IObservable<Unit> update = this.UpdateAsObservable();

            update.Subscribe(_ => HandleRotation());

            StartCoroutine(SpawnMole());

        }

        private List<GameObject> FillPool(int amount, GameObject prefab)
        {
            List<GameObject> pool = new List<GameObject>();
            GameObject tmp;
            // Fill pool with moles
            for (int i = 0; i < amount; i++)
            {
                tmp = Instantiate(prefab);
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
        
        private IEnumerator SpawnMole()
        {

            // Initial Wait
            yield return new WaitForSeconds(3f);
            // Game loop
            while (true)
            {
                // Wait between 1 and 3 seconds between waves
                yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
                 // Get a random number of moles to spawn
                int molesToSpawn = UnityEngine.Random.Range(1 , 4 );
                // Spawn moles
                for (int i = 0; i < molesToSpawn; i++)
                {
                    // Get random spot
                    int spot = UnityEngine.Random.Range(0, _spots.Length);
                    // Get mole from pool
                    GameObject mole = GetObjectFromPool(_molesPool);
                    // Set mole position to spot
                    mole.transform.position = _spots[spot].transform.position;
                    // Spot as a class and be responnsable for orientation and gameobject
                    
                    // Set parent to spot
                    mole.transform.SetParent(_spots[spot].transform);
                    // Activate mole
                    mole.SetActive(true);
                    // Wait between 0.5 and 1.5 seconds
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
                }
            }
        }
    }
}
