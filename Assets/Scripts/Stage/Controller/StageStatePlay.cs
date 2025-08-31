using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Stage.Controller
{
    public class StageStatePlay : IStageState
    {
        private readonly StageStateMachine sSM;
        private readonly StageController sC;

        public StageStatePlay(StageStateMachine sSM, StageController sC)
        {
            this.sSM = sSM;
            this.sC = sC;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            if (PlayerModel.Instance != null && StageModel.Instance.IsAllBlockFilled() && PlayerModel.Instance.IsOnExit)
                sSM.ChangeState(new StageStateClear(sC));
            else if (PlayerModel.Instance == null)
                sSM.ChangeState(new StageStateDead(sC));
            else if (CustomInputSystem.Instance.GetPauseKeyWithCooldown())
                sSM.ChangeState(new StageStatePause(sSM, sC));
        }

        public void OnStateExit()
        {

        }
    }
}