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
        [SerializeField] private Sprite filledSprite;
        [SerializeField] private Sprite emptySprite;
        [SerializeField] private Animator animator;
        private ObjectAnimation objectAnimation;
        [SerializeField] private GameObject hitBoxPrefab;
        private GameObject hitBoxObject;
        [SerializeField] private bool isWallInitial;

        public bool IsWallInitial => isWallInitial;

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

        public void DestroyHitBox()
        {
            Destroy(hitBoxObject);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UnityEditor.EditorApplication.update += SetViewOnEditor;
        }

        private void SetViewOnEditor()
        {
            UnityEditor.EditorApplication.update -= SetViewOnEditor;
            if(this == null)
                return;

            if (IsWallInitial)
                spriteRenderer.sprite = filledSprite;
            else
                spriteRenderer.sprite = emptySprite;
        }
#endif
    }
}
