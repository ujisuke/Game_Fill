using Assets.Scripts.Common.View;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using UnityEngine;

namespace Assets.Scripts.Stage.View
{
    [RequireComponent(typeof(BlockController))]
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        private ObjectAnimation objectAnimation;
        [SerializeField] private GameObject hitBoxPrefab;
        private GameObject hitBoxObject;

        public void SetPos(Vector2 pos)
        {
            transform.position = pos;
        }

        public void SetViewScale(Vector2 viewScale)
        {
            transform.localScale = viewScale;
        }

        public void PlayAnim(string animName, float animSeconds)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.Play(animName, animSeconds);
        }

        public void FlipX(bool isLeft)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.FlipX(isLeft);
        }

        public void InstantiateHitBox(HitBox hitBox)
        {
            hitBoxObject = Instantiate(hitBoxPrefab, hitBox.Pos, Quaternion.identity);
            hitBoxObject.transform.localScale = hitBox.Scale;
        }
        
        public void SetHitBoxActive(bool isActive)
        {
            hitBoxObject.SetActive(isActive);
        }

        public void Fill()
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }

        public void DestroyHitBox()
        {
            Destroy(hitBoxObject);
        }

        private void OnValidate()
        {
            if (GetComponent<BlockController>().IsWallOnFirst)
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            else
                spriteRenderer.color = new Color(0f, 0f, 0f, 0f);
        }
    }
}
