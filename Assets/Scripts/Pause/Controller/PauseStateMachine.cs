using UnityEngine;

namespace Assets.Scripts.Pause.Controller
{
    public class PauseStateMachine
    {
        private IPauseState currentState;

        public PauseStateMachine(PauseController pC)
        {
            currentState = new PauseStateInitial(pC, this);
            currentState.OnStateEnter();
        }

        public void ChangeState(IPauseState newState)
        {
            currentState.OnStateExit();
            currentState = newState;
            currentState.OnStateEnter();
        }

        public void HandleInput()
        {
            currentState.HandleInput();
        }
    }
}
