using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BAM.C
{
    public class BAMController : MonoBehaviour
    {
        // Init health
        public int health = 2;
        [SerializeField]
        private GameManager.GameState _gameStateToSwitchTo;
        private Material _material;
        private float _intensity = 5f;
        private BannerController _bannerController;

        void Start()
        {
            // Get material
            _material = GetComponent<Renderer>().material;
            // Set material color to white
            _material.color = Color.white;
            _material.EnableKeyword("_EMISSION");
            transform.DOShakePosition(1f, new Vector3(0.1f, 0.1f, 0f), 5, 90f, false, true).SetLoops(-1, LoopType.Yoyo);
            _bannerController = transform.parent.GetComponent<BannerController>(); 
        }

        // On collision with tag "Bullet" reduce health
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Bullet")
            {
                health -= other.gameObject.GetComponent<BulletController>().Power;
                // Dotween scale to 1.1
                transform.DOScale(1.1f, 0.1f);
                // Dotween change color intensity by 3 for 0.2 secs
                _material.DOColor(Color.white * _intensity, "_EmissionColor", 0.1f);
                // If health is 0, destroy the gameobject
                // set scale back to normal;
                transform.DOScale(1f, 0.1f);
                // set color back to normal
                _material.DOColor(Color.black, "_EmissionColor", 0.1f);

                Destroy(other.gameObject);

                if (health <= 0)
                {
                    DOTween.KillAll();
                    _bannerController.DestroyAndBanish(_gameStateToSwitchTo);
                    
                    Sequence sequence = DOTween.Sequence();
                    // Insert a scale tween and on complete switch
                    sequence.Insert(0, transform.DOLocalRotate(new Vector3(270f, 0f, 0f), 0.5f));
                    sequence.Insert(0, _material.DOFade(0f, 0.5f));
                    sequence.Insert(0,transform.DOScale(0f, 1f).OnComplete(()=> {
                        Destroy(gameObject, 2f);
                        }));

                }
            }
        }
    }
}
