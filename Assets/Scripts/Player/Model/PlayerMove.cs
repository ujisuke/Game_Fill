using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerMove
    {
        private readonly float defaultMoveSpeed;
        private readonly Vector2 pos;
        private readonly Vector2 moveDirPrev;
        public Vector2 Pos => pos;
        public Vector2 PosPrev => pos - moveDirPrev;

        public PlayerMove(Vector2 pos, float defaultMoveSpeed, Vector2 moveDirPrev)
        {
            this.pos = pos;
            this.defaultMoveSpeed = defaultMoveSpeed;
            this.moveDirPrev = moveDirPrev;
        }

        public PlayerMove(Vector2 pos, float defaultMoveSpeed)
        {
            this.pos = pos;
            this.defaultMoveSpeed = defaultMoveSpeed;
            moveDirPrev = Vector2.zero;
        }

        public PlayerMove MoveTurn(Vector2 moveDir)
        {
            Vector2 newPos = pos + defaultMoveSpeed * Time.deltaTime * moveDir;
            return new PlayerMove(newPos, defaultMoveSpeed, moveDir);
        }

        public PlayerMove MoveStraight()
        {
            Vector2 newPos = pos + defaultMoveSpeed * Time.deltaTime * moveDirPrev;
            return new PlayerMove(newPos, defaultMoveSpeed, moveDirPrev);
        }
    }
}
