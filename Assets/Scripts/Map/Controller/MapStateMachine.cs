using Assets.Scripts.Map.Data;
using UnityEngine;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateMachine
    {
        private IMapState currentState;

        public MapStateMachine(MapController mC)
        {
            currentState = new MapStateInitial(this, mC);
            currentState.OnStateEnter();
        }

        public void ChangeState(IMapState newState)
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
