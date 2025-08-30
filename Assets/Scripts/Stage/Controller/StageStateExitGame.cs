using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

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