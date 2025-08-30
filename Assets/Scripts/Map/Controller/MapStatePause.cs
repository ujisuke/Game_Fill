using Assets.Scripts.Pause.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Map.Controller
{
    public class MapStatePause : IMapState
    {
        private readonly MapStateMachine mSM;
        private readonly MapController mC;

        public MapStatePause(MapStateMachine stateMachine, MapController mC)
        {
            mSM = stateMachine;
            this.mC = mC;
        }

        public void OnStateEnter()
        {
            SceneManager.LoadScene(mC.PauseSceneName, LoadSceneMode.Additive);
            Time.timeScale = 0;
        }

        public void HandleInput()
        {
            if (PauseStateInitial.IsBack || PauseStateInitial.DoesSelectStage)
                mSM.ChangeState(new MapStateSelectStage(mSM, mC));
            else if (PauseStateInitial.DoesExitGame)
                mSM.ChangeState(new MapStateExitGame());
        }

        public void OnStateExit()
        {
            PauseStateInitial.ResetFlags();
            Time.timeScale = 1;
            SceneManager.UnloadSceneAsync(mC.PauseSceneName);
        }
    }
}
