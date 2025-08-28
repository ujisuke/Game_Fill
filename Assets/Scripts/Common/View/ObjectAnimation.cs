using UnityEngine;

namespace Assets.Scripts.Common.View
{
    public class ObjectAnimation
    {
        private readonly Animator animator;
        private readonly SpriteRenderer spriteRenderer;

        public ObjectAnimation(Animator animator, SpriteRenderer spriteRenderer)
        {
            this.animator = animator;
            this.spriteRenderer = spriteRenderer;
        }

        public void Play(string animName, float animSeconds)
        {
            if (animator == null)
                return;
            animator.Play(animName);
            if (animSeconds > 0f)
                animator.speed = 1f / animSeconds;
            else
                animator.speed = 1f;
        }

        public void SetSpeed(float speed)
        {
            if (animator != null)
                animator.speed = speed;
        }

        public void FlipX(bool isLeft)
        {
            if (spriteRenderer != null)
                spriteRenderer.flipX = isLeft;
        }

        public void Stop()
        {
            if (animator != null)
                animator.enabled = false;
        }
    }
}