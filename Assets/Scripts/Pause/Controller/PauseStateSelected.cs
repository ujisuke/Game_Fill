using UnityEngine.SceneManagement;

namespace Assets.Scripts.Pause.Controller
{
    public class PauseStateSelected : IPauseState
    {
        private readonly PauseController pC;

        public PauseStateSelected(PauseController pC)
        {
            this.pC = pC;
        }

        public void OnStateEnter()
        {
            SceneManager.UnloadSceneAsync(pC.PauseSceneName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}
