using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Model;
using UnityEngine;

namespace Assets.Scripts.Player.Controller
{
    public class PlayerStateMove : IPlayerState
    {
        private readonly PlayerModel pM;
        private readonly PlayerController pC;
        private readonly PlayerStateMachine pSM;
        private bool isLookingLeft;

        public PlayerStateMove(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM, bool isLookingLeft = false, bool isInitial = false)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
            this.isLookingLeft = isLookingLeft;


            if (!isInitial)
                return;
            if (CustomInputSystem.Instance.GetUpKey())
                pM.MoveTurn(Vector2.up);
            else if (CustomInputSystem.Instance.GetDownKey())
                pM.MoveTurn(Vector2.down);
            else if (CustomInputSystem.Instance.GetLeftKey())
                pM.MoveTurn(Vector2.left);
            else if (CustomInputSystem.Instance.GetRightKey())
                pM.MoveTurn(Vector2.right);
        }

        public void OnStateEnter()
        {
            pC.PlayAnim("Move");
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

            if (CustomInputSystem.Instance.GetSlowKeyDown())
                AudioSourceView.Instance.PlaySlowSE();
            if (CustomInputSystem.Instance.GetSlowKey())
            {
                pM.Deceleration();
                pC.PlayAnim("MoveSlow");
            }
            else
            {
                pM.Acceleration();
                pC.PlayAnim("Move");
            }

            if (StageModel.Instance.IsPlayerHittingWall(pM.HurtBox) || !StageModel.Instance.IsPlayerOnBlock(pM.Pos) || StageModel.Instance.TimeLimit <= 0)
                    pSM.ChangeState(new PlayerStateDead(pM, pC, pSM));
                else if (pM.IsOnExit)
                    pSM.ChangeState(new PlayerStateExit(pM, pC, pSM));
                else if (Input.GetMouseButton(0))
                    pSM.ChangeState(new PlayerStateFill(pM, pC, pSM, isLookingLeft));
        }

        public void OnStateExit()
        {

        }
    }
}