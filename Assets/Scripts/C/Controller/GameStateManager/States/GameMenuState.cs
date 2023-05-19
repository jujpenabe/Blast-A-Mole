using UnityEngine;

namespace BAM.C {

    public class GameMenuState : GameBaseState
    {
        public override void EnterState(GameStateManager gsm)
        {
            Debug.Log("Entering GameMenuState");
        }   

        public override void UpdateState(GameStateManager gsm)
        {
            Debug.Log("Updating GameMenuState");
        }
    }

}