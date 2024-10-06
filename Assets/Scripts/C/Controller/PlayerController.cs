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
            _targetSpot = _spots[4].transform; 
            // Replace Update() with UniRx UpdateAsObservable()
            IObservable<Unit> update = this.UpdateAsObservable();

            // Movement
            update.Subscribe(_ => HandleMovement());
            // change lerp duration momentarily
            var lerpDuration = _lerpDuration;
            _lerpDuration = 4.4f;
            // Lerp to position
            LerpToSpot(_targetSpot);
            // change lerp duration back to normal
            _lerpDuration = lerpDuration;
        }

        private void HandleMovement()
        {

            // If pressing Q | A or left arrow 
            if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
                _targetSpot = _spots[1].transform;
                _timeElapsed = 0;
            }
            // If pressing W | S or down arrow
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
                _targetSpot = _spots[2].transform;
                _timeElapsed = 0;
            }
            // If pressing E | D or right arrow
            if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
                _targetSpot = _spots[3].transform;
                _timeElapsed = 0;
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
