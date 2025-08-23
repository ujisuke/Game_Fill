using Assets.Scripts.Player.Model;
using UnityEngine;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateMachine
    {
        private IPlayerState currentState;

        public PlayerStateMachine(PlayerModel pM, PlayerController pC)
        {
            currentState = new PlayerStateBorn(pM, pC, this);
            currentState.OnStateEnter();
        }

        public void ChangeState(IPlayerState newState)
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
