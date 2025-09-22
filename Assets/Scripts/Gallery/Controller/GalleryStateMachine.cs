using Assets.Scripts.Map.Data;
using UnityEngine;

namespace Assets.Scripts.Gallery.Controller
{
    public class GalleryStateMachine
    {
        private IGalleryState currentState;

        public GalleryStateMachine(GalleryController gC)
        {
            currentState = new GalleryStateInitial(this, gC);
            currentState.OnStateEnter();
        }

        public void ChangeState(IGalleryState newState)
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
