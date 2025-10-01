using UnityEngine;

namespace Assets.Scripts.Farce.Controller
{
    public class FarceStateExitGame : IFarceState
    {

        public FarceStateExitGame()
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
