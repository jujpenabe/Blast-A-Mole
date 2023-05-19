using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using BAM.B;
using System;
using DG.Tweening;

namespace BAM.C
{
    public class MoleController : MonoBehaviour
    {

        //[SerializeField] private float _timer = 0.5f;
        [SerializeField] private int _health = 1;
        private void OnEnable()
        {
            DOTween.Init();
            ManageMovement();
        }

        private void ManageMovement()
        {
            var sequence = DOTween.Sequence();
            sequence.Insert(0, transform.DOShakePosition(0.3f, new Vector3(0.04f, 0.04f, 0), 10, 50, false, true));
            sequence.Insert(0.5f, transform.DOLocalMoveZ(0.3f, 1f));
            // rotate 30 degrees on local x axis
            sequence.Insert(1f, transform.DOLocalRotate(new Vector3(-30, 0, 0), 1f, RotateMode.Fast));
            // Shake rotation randomly on local z axis for 0.6 seconds
            sequence.Insert(1.7f, transform.DOShakeRotation(1f, new Vector3(0, 0, 120), 1, 90, true)); // Angle also based on health
            
            // calculate random number between 0 and 2
            float random = UnityEngine.Random.Range(0f, 2f);
            // Move back to original position
            sequence.Insert(2.9f + random, transform.DOShakePosition(0.3f, new Vector3(0.02f, 0.02f, 0), 10, 50, false, true));
            sequence.Insert(3.2f + random, transform.DOLocalRotate(new Vector3(0, 0, 0), 1f, RotateMode.Fast));
            sequence.Insert(3.4f + random, transform.DOLocalMoveZ(-1, 1f));
            // Hide on timer finish and destroy
            sequence.OnComplete(() => {
                gameObject.SetActive(false);
                });

        }

        public void HelmetHit(Collider other)
        {
            // Reduce health
            _health--;
            // Destroy bullet
            Destroy(other.gameObject);
            if (_health <= 0)
            {
                // Add score
                GameManager.S_Instance.AddScore(1);
                // Disable mole
                // And add score
                gameObject.SetActive(false);
            }
        }
    }
}
