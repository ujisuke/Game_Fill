using System;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateDead : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;

        public PlayerStateDead(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
        }

        public void OnStateEnter()
        {
            AudioSourceView.Instance.RestoreBGM();
            AudioSourceView.Instance.PlayDeadSE();
            Dead().Forget();
        }

        private async UniTask Dead()
        {
            pC.PlayAnim("Dead", 1f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.2f));
            PlayerModel.RemoveInstance();
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f));
            if (pC != null)
                pC.OnDestroy();
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}