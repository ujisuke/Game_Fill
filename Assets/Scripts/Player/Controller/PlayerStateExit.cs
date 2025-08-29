using System;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateExit : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;

        public PlayerStateExit(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
        }

        public void OnStateEnter()
        {
            if (!StageModel.Instance.IsAllBlockFilled())
                pSM.ChangeState(new PlayerStateDead(pM, pC, pSM));
            else
                pC.StopAnim();
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }
    }
}