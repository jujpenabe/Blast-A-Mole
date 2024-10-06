using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BAM.B;

namespace BAM
{
    public class HoleInstantiator : MonoBehaviour, IInstantiator
    {
        private GameObject _spawnable = null;
        //private Quaternion _lookRotation = Quaternion.identity;


        public Vector3 Position { get => transform.position; set => transform.position = value; }

        // Constructor Method
        public HoleInstantiator(GameObject spawnable)
        {
            _spawnable = spawnable;
        }

        // Constructor method null spawnable
        public HoleInstantiator()
        {
            _spawnable = null;
        }
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
            _spawnable.SetActive(true);
            _spawnable.transform.position = transform.position;
            _spawnable.transform.SetParent(transform);
            _spawnable.transform.rotation = transform.rotation;
            //_spawnable.transform.Rotate(new Vector3(-30, 0, 0)); // Fix rotation
        }

        public void LookAt(Vector3 pivot)
        {
            transform.rotation = Quaternion.LookRotation(transform.position - pivot);
            //_lookRotation = Quaternion.LookRotation(transform.position - pivot);
        }
    }
}
