using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Pause.Controller;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Volume.Controller
{
    public class VolumeStateExitPage : IVolumeState
    {
        private readonly VolumeController vC;
        private static bool doesExit;
        public static bool DoesExit => doesExit;

        public VolumeStateExitPage(VolumeController vC)
        {
            this.vC = vC;
        }

        public void OnStateEnter()
        {
            AudioSourceView.Instance.PlayChooseSE();
            doesExit = true;
            if (PauseStateSetVolume.FromPause)
                SceneManager.LoadScene(vC.PauseSceneName, LoadSceneMode.Additive);
            SceneManager.UnloadSceneAsync(vC.VolumeSceneName);
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }

        public static void ResetFlag()
        {
            doesExit = false;
        }
    }
}
