using System;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateClear : IStageState
    {
        private readonly StageController sC;
        private readonly StageStateMachine sSM;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public StageStateClear(StageController sC, StageStateMachine sSM)
        {
            this.sC = sC;
            this.sSM = sSM;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            Clear().Forget();
        }

        private async UniTask Clear()
        {
            if (sC.IsFinalStage)
            {
                await sC.PlayClearFinalEffect(token);
                SceneManager.LoadScene(sC.NextSceneName);
            }
            else if (sC.IsEndingStage)
                sSM.ChangeState(new StageStateEnding(sC, sSM));
            else
            {
                if(sC.NextSceneName[^1] == 'F')
                    AudioSourceView.Instance.FadeOutBGM().Forget();
                await sC.PlayClearEffect(token);
                SceneManager.LoadScene(sC.NextSceneName);
            }
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}