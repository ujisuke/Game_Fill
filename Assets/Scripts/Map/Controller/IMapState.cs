using UnityEngine;

namespace Assets.Scripts.Map.Controller
{
    public interface IMapState
    {
        void OnStateEnter();
        void HandleInput();
        void OnStateExit();
    }
}
