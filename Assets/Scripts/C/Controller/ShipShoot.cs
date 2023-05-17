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
        [SerializeField] private float _bulletSizeRatio = 1f;
        [SerializeField] private int _bulletPower = 1;
        // Increase rate
        [SerializeField] private float _bulletRadioIncreaseRate = 0.4f;
        // Increase force of bullet every 0.4 segs when holding shoot button down
        private float _elapsedTime = 0;
        private bool _instantiated, _increased;
        private Vector3 _localScale;
        private void Start()
        {
            _bullet = BulletPrefab;
            _localScale = transform.localScale;
        }
        private void Update()
        {
            if (!CanShoot()) return;
            if (Input.GetKeyDown(KeyCode.Space))
            {

            }
            if (Input.GetKey(KeyCode.Space))
            {
                if (!_instantiated) {
                    _bullet = Instantiate( BulletPrefab, _bulletSpawn.position, Quaternion.identity);
                    _bullet.transform.SetParent(transform);
                    _bullet.transform.DOShakePosition(_bulletRadioIncreaseRate, new Vector3(0.05f / _localScale.x, 0f / _localScale.y, 0.05f / _localScale.z) * _bulletSizeRatio , 10, 50, false, true).SetLoops(-1, LoopType.Yoyo);
                    _instantiated = true;
                }
                _bullet.transform.position = _bulletSpawn.position;
                _elapsedTime += Time.deltaTime;
                if (_elapsedTime >= _bulletRadioIncreaseRate && _bulletSizeRatio < 2 )
                {
                    _bulletSizeRatio+=0.5f;
                    _elapsedTime = 0;
                    _bulletPower++;
                    _bullet.transform.DOScale(new Vector3(1 / _localScale.x, 1 / _localScale.y, 1 / _localScale.z) * _bulletSizeRatio, _bulletRadioIncreaseRate * 0.5f).SetEase(Ease.OutBounce);
                }
                // Shake bullet based on force
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                if (!_instantiated) return;
                _bullet.transform.DOKill();
                _bullet.transform.parent = null;
                _lastShot = Time.time;
                _elapsedTime = 0;
                _bullet.Shoot(Vector3.forward, _bulletSpeed + (_bulletSizeRatio * 0.2f), _bulletPower);
                _instantiated = false;
                _bulletSizeRatio = 1;
                _bulletPower = 1;
            }
        }

        private bool CanShoot()
        {
            if (Time.time <= _lastShot + _fireRate) return false;
            return true;
        }

    }
}
