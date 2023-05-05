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
        [SerializeField] private int _molesToPool = 5;
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
        private void Awake()
        {
            // Instantiate Moles for pool
            for (int i = 0; i < _spots.Length; i++)
            {
                GameObject mole = Instantiate(Resources.Load("Prefabs/Mole")) as GameObject;
                mole.transform.SetParent(_spots[i].transform);
                mole.transform.localPosition = Vector3.zero;
                mole.transform.localScale = Vector3.one;
                mole.SetActive(false);
            }
        }
        private void Start()
        {
            _molesPool = new List<GameObject>();
            GameObject tmp;
            // Fill pool with moles
            for (int i = 0; i < _molesToPool; i++)
            {
                tmp = Instantiate(_molePrefab);
                tmp.SetActive(false);
                _molesPool.Add(tmp);
            }

            IObservable<Unit> update = this.UpdateAsObservable();

            update.Subscribe(_ => HandleRotation());

            
        }

        public GameObject GetMoleFromPool()
        {
            for (int i = 0; i < _molesPool.Count; i++)
            {
                if (!_molesPool[i].activeInHierarchy)
                {
                    return _molesPool[i];
                }
            }
            return null;
        }
        private void HandleRotation()
        {   
            transform.Rotate(new Vector3(_rotationSpeed * Time.deltaTime, 0, _rotationSpeed * Time.deltaTime));
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
                    GameObject mole = GetMoleFromPool();
                    // Set mole position to spot
                    mole.transform.position = _spots[spot].transform.position;
                    // Activate mole
                    mole.SetActive(true);
                    // Wait between 0.5 and 1.5 seconds
                    yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f));
                }
            }
        }
    }
}
