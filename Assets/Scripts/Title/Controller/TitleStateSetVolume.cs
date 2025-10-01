using Assets.Scripts.Volume.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Title.Controller
{
    public class TitleStateSetVolume : ITitleState
    {
        private readonly TitleController tC;
        private readonly TitleStateMachine tSM;


        public TitleStateSetVolume(TitleController tC, TitleStateMachine tSM)
        {
            this.tC = tC;
            this.tSM = tSM;
        }

        public void OnStateEnter()
        {
            SceneManager.LoadScene(tC.VolumeSceneName, LoadSceneMode.Additive);
            tC.SetActiveButtons(false);
            Time.timeScale = 0f;
        }

        public void HandleInput()
        {
            if (VolumeStateExitPage.DoesExit)
                tSM.ChangeState(new TitleStateInitial(tC, tSM, true));
        }

        public void OnStateExit()
        {
            VolumeStateExitPage.ResetFlag();
            Time.timeScale = 1f;
        }
    }
}
