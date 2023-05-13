using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BAM
{
    public class HoleInstantiator : MonoBehaviour, IInstantiator
    {
        private GameObject _spawnable;
        private Quaternion _lookRotation = Quaternion.identity;


        public Vector3 Position { get => transform.position; set => transform.position = value; }

        public object GetSpawnable()
        {
            return _spawnable;
        }

        public void SetSpawnable(object spawnable)
        {
            _spawnable = (GameObject)spawnable;
        }

        public void Spawn()
        {
            _spawnable.transform.position = transform.position;

            _spawnable.transform.SetParent(transform);
            _spawnable.transform.rotation = _lookRotation;
            _spawnable.transform.Rotate(new Vector3(90, 0, 0)); // Fix rotation
            // Activate spawnable instead of instantiating
            _spawnable.SetActive(true);
        }

        public void LookAt(Vector3 pivot)
        {
            _lookRotation = Quaternion.LookRotation(transform.position - pivot);
        }
    }
}
