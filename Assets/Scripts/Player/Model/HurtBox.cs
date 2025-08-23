using UnityEngine;

namespace Assets.Scripts.Player.Model
{
    public class HurtBox
    {
        private readonly Vector2 pos;
        private readonly Vector2 scale;
        public Vector2 Pos => pos;
        public Vector2 Scale => scale;
        public Vector2 LeftTopPos => new(pos.x - scale.x / 2f, pos.y + scale.y / 2f);
        public Vector2 RightBottomPos => new(pos.x + scale.x / 2f, pos.y - scale.y / 2f);

        public HurtBox(Vector2 pos, Vector2 scale)
        {
            this.pos = pos;
            this.scale = scale;
        }

        public HurtBox SetPos(Vector2 pos)
        {
            return new HurtBox(pos, scale);
        }
    }
}
