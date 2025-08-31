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
        private bool isDirKeyPushed;

        public PlayerStateFill(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM, bool isLookingLeft = false)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
            this.isLookingLeft = isLookingLeft;
            isDirKeyPushed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);
        }

        public void OnStateEnter()
        {
            pC.PlayAnim("Fill");
        }

        public void HandleInput()
        {
            if (isDirKeyPushed)
                pM.MoveStraight();
            else
            {
                if (Input.GetKey(KeyCode.W))
                    pM.MoveTurn(Vector2.up);
                else if (Input.GetKey(KeyCode.S))
                    pM.MoveTurn(Vector2.down);
                else if (Input.GetKey(KeyCode.A))
                    pM.MoveTurn(Vector2.left);
                else if (Input.GetKey(KeyCode.D))
                    pM.MoveTurn(Vector2.right);
                else
                    pM.MoveStraight();
                isLookingLeft = Input.GetKey(KeyCode.A) || (isLookingLeft && !Input.GetKey(KeyCode.D));
                pC.FlipX(isLookingLeft);
            }
            StageModel.Instance.FillBlock(pM.HurtBox);

            isDirKeyPushed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);


            if (Input.GetMouseButton(1))
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

        }
    }
}