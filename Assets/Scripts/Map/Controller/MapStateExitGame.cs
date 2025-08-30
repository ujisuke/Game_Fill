using UnityEngine;

namespace Assets.Scripts.Map.Controller
{
    public class MapStateExitGame : IMapState
    {
        public MapStateExitGame()
        {

        }

        public void OnStateEnter()
        {
            Application.Quit();
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
