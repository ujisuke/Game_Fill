using UnityEngine;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateMachine
    {
        private ITitleState currentState;

        public TitleStateMachine(TitleController tC)
        {
            currentState = new TitleStateInitial(tC, this);
            currentState.OnStateEnter();
        }

        public void ChangeState(ITitleState newState)
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
