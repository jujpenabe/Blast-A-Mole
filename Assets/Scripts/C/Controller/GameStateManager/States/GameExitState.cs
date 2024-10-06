using UnityEngine;

namespace BAM.C {

    public class GameExitState : GameBaseState
    {
        public override void EnterState(GameStateManager gsm)
        {
            Application.Quit();
        }   

        public override void UpdateState(GameStateManager gsm)
        {
            Debug.Log("Updating GameMenuState");
        }
    }

}