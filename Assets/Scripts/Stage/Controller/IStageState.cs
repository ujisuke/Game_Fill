using UnityEngine;

namespace Assets.Scripts.Stage.Controller
{
    public interface IStageState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
