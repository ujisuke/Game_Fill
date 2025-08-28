using System;
using Assets.Scripts.Player.Model;
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
            pC.PlayAnim("Dead", 1f);
            Dead().Forget();
        }

        private async UniTask Dead()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
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