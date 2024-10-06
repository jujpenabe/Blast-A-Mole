using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BAM.C
{
    public class GameStateManager : MonoBehaviour
    {
        
        public GameBaseState currentState;
        public GameMenuState gameMenuState = new GameMenuState();
        public GamePlayState gamePlayState = new GamePlayState();
        public GameIntroState gameIntroState = new GameIntroState();
        public GameExitState gameExitState = new GameExitState();


        void Start()
        {
            currentState = gameIntroState;
            currentState.EnterState(this);
        }

        void Update()
        {
            currentState.UpdateState(this);
        }

        public void SwitchState(GameBaseState newState)
        {
            currentState = newState;
            currentState.EnterState(this);
        }

    }
}
