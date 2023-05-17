using System.Collections;
using System.Collections.Generic;
using BAM.B;
using DG.Tweening;
using UnityEngine;

namespace BAM.C
{
    public class ShipShoot : MonoBehaviour
    {
        // Prefab of bullet
        public BulletController BulletPrefab;
        private BulletController _bullet;
        [SerializeField] private float _fireRate = 0.2f;
        private float _lastShot = 0;
        // Position of bullet spawn
        [SerializeField] private Transform _bulletSpawn;
        // Initial force of bullet
        [SerializeField] private int _bulletSpeed = 1;
        [SerializeField] private int _bulletSizeRatio = 1;
        // Increase rate
        [SerializeField] private float _bulletRadioIncreaseRate = 0.4f;
        // Increase force of bullet every 0.4 segs when holding shoot button down
        private float _elapsedTime = 0;
        private bool _instantiated, _increased;
        // Set _bulletPrefab to BulletPrefab
        private void Start()
        {
            _bullet = BulletPrefab;
        }
        private void Update()
        {
            if (!CanShoot()) return;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _bullet = Instantiate( BulletPrefab, _bulletSpawn.position, Quaternion.identity);
                _instantiated = true;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (!_instantiated) {
                    _bullet = Instantiate( BulletPrefab, _bulletSpawn.position, Quaternion.identity);
                }
                _instantiated = true;
                _bullet.transform.position = _bulletSpawn.position;
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= _bulletRadioIncreaseRate && _bulletSizeRatio < 3 )
                {
                    _bulletSizeRatio++;
                    _elapsedTime = 0;
                    // DOTween scale bounce bullet based on force
                    //DOTween.To(() => _bullet.transform.localScale, x => _bullet.transform.localScale = x, new Vector3(1, 1, 1) * _bulletSizeRatio, _bulletRadioIncreaseRate * 0.5f).SetEase(Ease.OutBounce);
                    _bullet.transform.DOScale(new Vector3(1, 1, 1) * _bulletSizeRatio, _bulletRadioIncreaseRate * 0.5f).SetEase(Ease.OutBounce);
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!_instantiated) return;
                Debug.Log("Shoot");
                _lastShot = Time.time;
                _elapsedTime = 0;
                _bullet.Shoot(Vector3.forward, _bulletSpeed + (_bulletSizeRatio * 0.1f));
                _bulletSizeRatio = 1;
                _instantiated = false;
            }
        }

        private bool CanShoot()
        {
            if (Time.time <= _lastShot + _fireRate) return false;
            return true;
        }

    }
}
