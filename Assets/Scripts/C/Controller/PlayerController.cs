using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace BAM.C
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private KeyCode[] _keys = new KeyCode[3];
        [SerializeField] private GameObject[] _spots = new GameObject[3];
        [SerializeField] private float _lerpDuration = 0.2f;
        private float _timeElapsed = 0;
        private bool _lerping = false;
        private Transform _currentSpot;
        private Transform _targetSpot;
        private void Start()
        {
            // Set current spot to first spot
            _currentSpot = _spots[0].transform;
            _targetSpot = _spots[0].transform;
            // Replace Update() with UniRx UpdateAsObservable()
            IObservable<Unit> update = this.UpdateAsObservable();

            // Movement
            update.Subscribe(_ => HandleMovement());
        }

        private void HandleMovement()
        {
            // Get input from keyboard for three spots
            for (int i = 0; i < _keys.Length; i++)
            {   
                if (Input.GetKeyDown(_keys[i])){
                    _targetSpot = _spots[i].transform;
                    _timeElapsed = 0;
                    break;
                }
            }
            // Move to spot
            LerpToSpot(_targetSpot);
        }

        // This move class should be in his controller
        private void LerpToSpot(Transform spot)
        {
            if (spot == _currentSpot && !_lerping) return;
            if (_timeElapsed < _lerpDuration){
                _lerping = true;
                transform.position = Vector3.Lerp(transform.position, spot.position, _timeElapsed / _lerpDuration);
                _timeElapsed += Time.deltaTime;
            } else {
                _lerping = false;
                transform.position = spot.position;
                _currentSpot = spot;
            }
            
        }
    }
}
