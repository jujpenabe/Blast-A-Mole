using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace BAM.C
{
    public class BannerController : MonoBehaviour
    {
        // flag when the banner is started
        public bool presented = false;

        [SerializeField] private GameObject _banner;
        [SerializeField] private GameObject _BAM_B;
        [SerializeField] private GameObject _BAM_A;
        [SerializeField] private GameObject _BAM_M;
        [SerializeField] private GameManager.GameState _gameStateToSwitchTo;
        void Start()
        {
            transform.localScale = Vector3.zero;
            // Init sequence
            //Sequence sequence = DOTween.Sequence();
            // Insert a scale tween and on complete switch
            transform.DOScale(1.5f, 3f).SetEase(Ease.OutBounce).OnComplete(()=>Presented());
            // Shake the banner a little bit in loop
            transform.DOShakePosition(3f, 0.05f, 10, 90f, false, true).SetLoops(-1, LoopType.Yoyo);

        }

        public void DestroyAndBanish(GameManager.GameState switchToState)
        {
            DOTween.KillAll();
            // Try get maerial of _banner and fadeout with dotween
            // Scale _banner to 6
            if (_banner != null) _banner.transform.DOScale(6f, 0.4f);
            if (_banner != null) _banner.GetComponent<Renderer>().material.DOFade(0f, 0.4f);
            if (_BAM_B != null) _BAM_B.GetComponent<Renderer>().material.DOFade(0f, 0.5f);
            if (_BAM_A != null) _BAM_A.GetComponent<Renderer>().material.DOFade(0f, 0.5f);
            if (_BAM_M != null) _BAM_M.GetComponent<Renderer>().material.DOFade(0f, 0.5f);
            _gameStateToSwitchTo = switchToState;
            Destroy(gameObject, 1f);
        }

        private void Presented()
        {
            presented = true;
        }
        private void OnDestroy()
        {
            GameManager.S_Instance.SwitchState(_gameStateToSwitchTo);
        }
    }
}
