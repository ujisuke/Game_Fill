using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateBorn : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;

        public PlayerStateBorn(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
        }

        public void OnStateEnter()
        {
            
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetLeftKey() || CustomInputSystem.Instance.GetRightKey() || CustomInputSystem.Instance.GetUpKey() || CustomInputSystem.Instance.GetDownKey())
                pSM.ChangeState(new PlayerStateMove(pM, pC, pSM, isInitial: true));
        }

        public void OnStateExit()
        {
            StageModel.Instance.CountDownTimer().Forget();
        }
    }
}