using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateClear : IStageState
    {
        private readonly StageStateMachine sSM;

        public StageStateClear(StageStateMachine sSM)
        {
            this.sSM = sSM;
        }

        public void OnStateEnter()
        {
            if (sSM.IsFinalStage)
                SceneManager.LoadScene(sSM.NextStageName);
            else
                SceneManager.LoadScene(sSM.MapSceneName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}