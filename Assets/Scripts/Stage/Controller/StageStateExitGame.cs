using UnityEngine;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateExitGame : IStageState
    {

        public StageStateExitGame()
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