using Assets.Scripts.Player.Data;
using Assets.Scripts.Stage.Model;
using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerModel
    {
        private HurtBox hurtBox;
        private PlayerMove playerMove;
        private static PlayerModel instance;
        public Vector2 Pos => playerMove.Pos;
        public HurtBox HurtBox => hurtBox;
        public bool IsOnExit => StageModel.Instance.IsPlayerOnExit(Pos);
        public float CurrentDecelerationFactor => playerMove.CurrentDecelerationFactor;
        public static PlayerModel Instance => instance;

        public PlayerModel(PlayerData playerData, Vector2 pos)
        {
            playerMove = new PlayerMove(pos, playerData.MoveSpeed);
            hurtBox = new HurtBox(pos, playerData.HurtBoxScale);
            instance = this;
        }

        public void MoveTurn(Vector2 moveDir)
        {
            playerMove = playerMove.MoveTurn(moveDir);
            hurtBox = hurtBox.SetPos(Pos);
        }

        public void MoveStraight()
        {
            playerMove = playerMove.MoveStraight();
            hurtBox = hurtBox.SetPos(Pos);
        }

        public void Deceleration()
        {
            playerMove = playerMove.Deceleration();
            StageModel.Instance.PlaySlowEffect();
        }

        public void Acceleration()
        {
            playerMove = playerMove.Acceleration();
            StageModel.Instance.StopSlowEffect();
        }

        public static void RemoveInstance()
        {
            instance = null;
        }
    }
}
