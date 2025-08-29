using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class PlayerMove
    {
        private readonly float moveSpeedDefault;
        private readonly float moveSpeedCurrent;
        private readonly Vector2 pos;
        private readonly Vector2 moveDirPrev;
        private readonly float decelerationFactor;
        public Vector2 Pos => pos;
        public Vector2 PosPrev => pos - moveDirPrev;
        public float CurrentDecelerationFactor => moveSpeedCurrent / moveSpeedDefault;

        public PlayerMove(Vector2 pos, float moveSpeedDefault, float moveSpeedCurrent, Vector2 moveDirPrev, float decelerationFactor)
        {
            this.pos = pos;
            this.moveSpeedDefault = moveSpeedDefault;
            this.moveSpeedCurrent = moveSpeedCurrent;
            this.moveDirPrev = moveDirPrev;
            this.decelerationFactor = decelerationFactor;
        }

        public PlayerMove(Vector2 pos, float moveSpeedDefault)
        {
            this.pos = pos;
            this.moveSpeedDefault = moveSpeedDefault;
            moveSpeedCurrent = moveSpeedDefault;
            moveDirPrev = Vector2.zero;
            decelerationFactor = 0.1f;
        }

        public PlayerMove MoveTurn(Vector2 moveDir)
        {
            Vector2 newPos = pos + moveSpeedCurrent * Time.deltaTime * moveDir;
            return new PlayerMove(newPos, moveSpeedDefault, moveSpeedCurrent, moveDir, decelerationFactor);
        }

        public PlayerMove MoveStraight()
        {
            Vector2 newPos = pos + moveSpeedCurrent * Time.deltaTime * moveDirPrev;
            return new PlayerMove(newPos, moveSpeedDefault, moveSpeedCurrent, moveDirPrev, decelerationFactor);
        }

        public PlayerMove Deceleration()
        {
            return new PlayerMove(pos, moveSpeedDefault, moveSpeedDefault * decelerationFactor, moveDirPrev, decelerationFactor);
        }

        public PlayerMove Acceleration()
        {
            return new PlayerMove(pos, moveSpeedDefault, moveSpeedDefault, moveDirPrev, decelerationFactor);
        }
    }
}
