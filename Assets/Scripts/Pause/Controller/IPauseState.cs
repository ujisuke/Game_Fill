using UnityEngine;

namespace Assets.Scripts.Pause.Controller
{
    public interface IPauseState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
