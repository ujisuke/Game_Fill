using System.Threading;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStateLoadMap : IStageState
    {
        private readonly StageController sC;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;

        public StageStateLoadMap(StageController sC)
        {
            this.sC = sC;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            Load().Forget();
        }

        private async UniTask Load()
        {
            await sC.CloseStage(false, token);
            SceneManager.LoadScene(sC.MapSceneName);
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