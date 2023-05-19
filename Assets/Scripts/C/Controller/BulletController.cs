using System;
using System.Collections;
using System.Collections.Generic;
using BAM.B;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace BAM
{
    public class BulletController : MonoBehaviour, IShootable
    {
        // Set bullet direction and speed variables with getters and setters

        // Subscribe update method on Awake
        IObservable<Unit> updateAsObservable;

        private Vector3 _direction;
        public Vector3 Direction { get => _direction; set => _direction = value; }
        private float _speed;
        public float Speed { get => _speed; set => _speed = value; }
        private int _power;
        public int Power { get => _power; set => _power = value; }

        private void Awake()
        {
            updateAsObservable = this.UpdateAsObservable();
        }

        private void HandleMovement()
        {
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }

        public void Shoot(Vector3 direction, float speed, int power)
        {
            updateAsObservable.Subscribe(_ => HandleMovement());
            _speed = speed;
            _power = power;
            _direction = direction.normalized;
            // destroy after 3 secs
            Destroy(gameObject, 3f);
        }
    }
}
