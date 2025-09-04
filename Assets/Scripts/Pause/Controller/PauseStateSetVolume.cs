using Assets.Scripts.Common.Controller;
using Assets.Scripts.Pause.Controller;
using Assets.Scripts.Volume.Controller;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Pause.Controller
{
    public class PauseStateSetVolume : IPauseState
    {
        private readonly PauseController pC;
        private static bool fromPause = false;
        public static bool FromPause => fromPause;

        public PauseStateSetVolume(PauseController pC)
        {
            this.pC = pC;
            fromPause = true;
        }

        public void OnStateEnter()
        {
            SceneManager.LoadScene(pC.VolumeSceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(pC.PauseSceneName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {
            
        }

        public static void ResetFlag()
        {
            fromPause = false;
        }
    }
}
