using UnityEngine;

namespace BAM.C {

    public class GameIntroState : GameBaseState
    {
        private BannerController _banner;
        public override void EnterState(GameStateManager gsm)
        {
            // Instantiate a new Banner Controller
            //Instantiate(gsm.bannerController);
            _banner = GameManager.S_Instance.bannerPrefab;
            // Instantiate at 0, 5, 4
            _banner = GameManager.Instantiate(_banner, new Vector3(0f, 5f, 4f), _banner.transform.rotation);
        }   

        public override void UpdateState(GameStateManager gsm)
        {
            if (_banner.presented)
            {
                gsm.SwitchState(gsm.gameMenuState);
            }
        }
    }
}