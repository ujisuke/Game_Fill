using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using UnityEngine;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateFill : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;
        private bool isLookingLeft;

        public PlayerStateFill(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM, bool isLookingLeft = false)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
            this.isLookingLeft = isLookingLeft;
        }

        public void OnStateEnter()
        {
            AudioSourceView.Instance.PlayFillSE();
            pC.PlayAnim("Fill");
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetUpKeyDown())
            {
                AudioSourceView.Instance.PlayTurnSE();
                pM.MoveTurn(Vector2.up);
            }
            else if (CustomInputSystem.Instance.GetDownKeyDown())
            {
                AudioSourceView.Instance.PlayTurnSE();
                pM.MoveTurn(Vector2.down);
            }
            else if (CustomInputSystem.Instance.GetLeftKeyDown())
            {
                AudioSourceView.Instance.PlayTurnSE();
                pM.MoveTurn(Vector2.left);
            }
            else if (CustomInputSystem.Instance.GetRightKeyDown())
            {
                AudioSourceView.Instance.PlayTurnSE();
                pM.MoveTurn(Vector2.right);
            }
            else
                pM.MoveStraight();
            isLookingLeft = CustomInputSystem.Instance.GetLeftKey() || (isLookingLeft && !CustomInputSystem.Instance.GetRightKey());
            pC.FlipX(isLookingLeft);
            
            StageModel.Instance.FillBlock(pM.HurtBox);

            if (CustomInputSystem.Instance.GetSlowKeyDown())
                AudioSourceView.Instance.PlaySlowSE();
            if (CustomInputSystem.Instance.GetSlowKey())
            {
                pM.Deceleration();
                pC.PlayAnim("FillSlow");
            }
            else
            {
                pM.Acceleration();
                pC.PlayAnim("Fill");
            }

            if (StageModel.Instance.IsPlayerHittingWall(pM.HurtBox) || !StageModel.Instance.IsPlayerOnBlock(pM.Pos) || StageModel.Instance.TimeLimit <= 0)
                pSM.ChangeState(new PlayerStateDead(pM, pC, pSM));
            else if (pM.IsOnExit)
                pSM.ChangeState(new PlayerStateExit(pM, pC, pSM));
            else if (!Input.GetMouseButton(0))
                pSM.ChangeState(new PlayerStateMove(pM, pC, pSM, isLookingLeft));
        }

        public void OnStateExit()
        {
            AudioSourceView.Instance.StopFillSE();
        }
    }
}