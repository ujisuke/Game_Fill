using Assets.Scripts.Common.View;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common.View
{
    public class ImageView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] protected SpriteRenderer dummySpriteRenderer;
        [SerializeField] private Image image;
        private ObjectAnimation objectAnimation;

        private void Awake()
        {
            objectAnimation = new ObjectAnimation(animator, dummySpriteRenderer);
        }

        protected void Update()
        {
            image.enabled = dummySpriteRenderer.enabled;
            image.sprite = dummySpriteRenderer.sprite;
            image.rectTransform.sizeDelta = dummySpriteRenderer.sprite.textureRect.size;
            image.rectTransform.pivot = dummySpriteRenderer.sprite.pivot / dummySpriteRenderer.sprite.rect.size;
        }

        public void PlayAnim(string animName, float animSeconds = 1f)
        {
            objectAnimation ??= new ObjectAnimation(animator, dummySpriteRenderer);
            objectAnimation.Play(animName, animSeconds);
        }

        public void SetSprite(Sprite sprite)
        {
            image.sprite = sprite;
            dummySpriteRenderer.sprite = sprite;
        }

        public void SetColor(Color color)
        {
            image.color = color;
        }

        public void Initialize(Sprite sprite)
        {
            dummySpriteRenderer.enabled = true;
            image.enabled = true;
            SetSprite(sprite);
        }
    }
}
