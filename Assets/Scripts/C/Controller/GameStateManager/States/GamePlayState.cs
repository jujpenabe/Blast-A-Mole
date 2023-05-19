using DG.Tweening;
using UnityEngine;

namespace BAM.C {

    public class GamePlayState : GameBaseState
    {
        private PlanetController _planet;
        public override void EnterState(GameStateManager gsm)
        {
            Debug.Log("Entering GamePlayState");
            // Instantiate planet and move with dotween to center
            _planet = GameManager.S_Instance.planetPrefab;
            _planet = GameManager.Instantiate(_planet, new Vector3(0f, 0f, 25f), _planet.transform.rotation);
            _planet.transform.DOMove(new Vector3(0f,0f, 10f) , 2f).SetEase(Ease.OutBounce);
            GameManager.S_Instance.PlayAudio();
        }   

        public override void UpdateState(GameStateManager gsm)
        {
            Debug.Log("Updating GamePlayState");
        }
    }

}