using Assets.Scripts.Common.View;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Stage.View
{
    public class ImageView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer dummySpriteRenderer;
        [SerializeField] private Image image;
        private ObjectAnimation objectAnimation;

        private void Awake()
        {
            objectAnimation = new ObjectAnimation(animator, dummySpriteRenderer);
        }

        private void Update()
        {
            image.enabled = dummySpriteRenderer.enabled;
            image.sprite = dummySpriteRenderer.sprite;
            image.rectTransform.sizeDelta = dummySpriteRenderer.sprite.textureRect.size;
            image.rectTransform.pivot = dummySpriteRenderer.sprite.pivot / dummySpriteRenderer.sprite.rect.size;
        }

        public void PlayAnim(string animName, float animSeconds = 1f)
        {
            objectAnimation.Play(animName, animSeconds);
        }

        public void SetAnimSpeed(float seconds)
        {
            objectAnimation.SetSpeed(seconds);
        }

        public void FlipX(bool isLeft)
        {
            objectAnimation.FlipX(isLeft);
        }
    }
}
