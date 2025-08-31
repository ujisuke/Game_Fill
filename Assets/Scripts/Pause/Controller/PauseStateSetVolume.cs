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
        private readonly PauseStateMachine pSM;


        public PauseStateSetVolume(PauseController pC, PauseStateMachine pSM)
        {
            this.pC = pC;
            this.pSM = pSM;
        }

        public void OnStateEnter()
        {
            SceneManager.LoadScene(pC.VolumeSceneName, LoadSceneMode.Additive);
            pC.SetActiveButtons(false);
        }

        public void HandleInput()
        {
            if (VolumeStateExitPage.DoesExit)
                pSM.ChangeState(new PauseStateInitial(pC, pSM));
        }

        public void OnStateExit()
        {
            VolumeStateExitPage.ResetFlag();
            SceneManager.UnloadSceneAsync(pC.VolumeSceneName);
        }
    }
}
