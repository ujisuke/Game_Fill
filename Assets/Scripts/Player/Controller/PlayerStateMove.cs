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
        private bool isKeyPushed;

        public PlayerStateMove(PlayerModel pM, PlayerController pC, PlayerStateMachine pSM)
        {
            this.pM = pM;
            this.pC = pC;
            this.pSM = pSM;
            isLookingLeft = false;
        }

        public void OnStateEnter()
        {

        }

        public void HandleInput()
        {
            if (isKeyPushed)
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

            isKeyPushed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D);

            if(StageModel.Instance.IsPlayerHittingWall(pM.HurtBox))
                pSM.ChangeState(new PlayerStateDead(pM, pC, pSM));
        }

        public void OnStateExit()
        {

        }
    }
}