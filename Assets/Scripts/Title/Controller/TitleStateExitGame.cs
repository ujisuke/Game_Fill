using UnityEngine;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateExitGame : ITitleState
    {

        public TitleStateExitGame()
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
