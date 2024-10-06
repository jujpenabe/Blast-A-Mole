using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BAM.C
{
    public class HelmetManager : MonoBehaviour
    {
        private MoleController parent;

        private void Awake()
        {
            parent = GetComponentInParent<MoleController>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Bullet"))
            {
                parent.HelmetHit(other);
            }
        }
    }
}
