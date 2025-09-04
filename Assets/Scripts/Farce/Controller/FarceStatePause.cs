using Assets.Scripts.Pause.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Farce.Controller
{
    public class FarceStatePause : IFarceState
    {
        private readonly FarceStateMachine fSM;
        private readonly FarceController fC;

        public FarceStatePause(FarceStateMachine fSM, FarceController fC)
        {
            this.fSM = fSM;
            this.fC = fC;
        }

        public void OnStateEnter()
        {
            SceneManager.LoadScene(fC.PauseSceneName, LoadSceneMode.Additive);
            Time.timeScale = 0;
        }

        public void HandleInput()
        {
            if (PauseStateInitial.IsBack)
                fSM.ChangeState(new FarceStateInitial(fSM, fC));
            else if (PauseStateInitial.DoesSelectStage)
                fSM.ChangeState(new FarceStateLoadMap(fC));
            else if (PauseStateInitial.DoesExitGame)
                fSM.ChangeState(new FarceStateExitGame());
        }

        public void OnStateExit()
        {
            PauseStateInitial.ResetFlags();
            Time.timeScale = 1;
        }
    }
}