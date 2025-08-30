using Assets.Scripts.Pause.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStatePause : IStageState
    {
        private readonly StageStateMachine sSM;
        private readonly StageController sC;

        public StageStatePause(StageStateMachine sSM, StageController sC)
        {
            this.sSM = sSM;
            this.sC = sC;
        }

        public void OnStateEnter()
        {
            SceneManager.LoadScene(sC.PauseSceneName, LoadSceneMode.Additive);
            Time.timeScale = 0;
        }

        public void HandleInput()
        {
            if (PauseStateInitial.IsBack)
                sSM.ChangeState(new StageStatePlay(sSM, sC));
            else if (PauseStateInitial.DoesSelectStage)
                sSM.ChangeState(new StageStateSelectStage(sSM, sC));
            else if (PauseStateInitial.DoesExitGame)
                sSM.ChangeState(new StageStateExitGame());
        }

        public void OnStateExit()
        {
            PauseStateInitial.ResetFlags();
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(sC.PauseSceneName);
        }
    }
}