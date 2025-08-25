using Assets.Scripts.Player.Data;
using Assets.Scripts.Stage.Model;
using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerModel
    {
        private readonly PlayerData playerData;
        private HurtBox hurtBox;
        private PlayerMove playerMove;
        private static PlayerModel instance;
        public Vector2 Pos => playerMove.Pos;
        public HurtBox HurtBox => hurtBox;
        public static PlayerModel Instance => instance;

        public PlayerModel(PlayerData playerData, Vector2 pos)
        {
            this.playerData = playerData;
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

        public void Destroy()
        {
            instance = null;
        }
    }
}
