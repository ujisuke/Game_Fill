using Assets.Scripts.Common.View;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.Stage.View
{
    [RequireComponent(typeof(BlockController))]
    public class BlockView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite filledSprite;
        [SerializeField] private Sprite prohibitedSprite;
        [SerializeField] private Sprite emptySprite;
        [SerializeField] private Sprite enterSprite;
        [SerializeField] private Sprite exitSprite;
        [SerializeField] private Animator animator;
        private ObjectAnimation objectAnimation;
        [SerializeField] private GameObject hitBoxPrefab;
        private GameObject hitBoxObject;
        [SerializeField] private BlockType blockType;
        [SerializeField] private Light2D light2D;

        public bool IsWallInitial => blockType == BlockType.Filled || blockType == BlockType.Prohibited;
        public bool CanBeFilled => blockType == BlockType.Empty;
        public bool IsExit => blockType == BlockType.Exit;

        public void PlayAnim(string animName, float animSeconds = 1f)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.Play(animName, animSeconds);
            if (animName == "Filled" || animName == "Filling")
                light2D.enabled = true;
        }

        public void SetAnimSpeed(float seconds)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.SetSpeed(seconds);
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

        public void SetInitialAnim()
        {
            light2D.enabled = false;
            if (blockType == BlockType.Filled)
                PlayAnim("Filled");
            else if (blockType == BlockType.Prohibited)
                PlayAnim("Prohibited");
            else if (blockType == BlockType.Enter)
                PlayAnim("Enter");
            else if (blockType == BlockType.Exit)
                PlayAnim("Exit");
            else
                PlayAnim("Empty");
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            UnityEditor.EditorApplication.update += SetViewOnEditor;
        }

        private void SetViewOnEditor()
        {
            UnityEditor.EditorApplication.update -= SetViewOnEditor;
            if (this == null)
                return;
            if (blockType == BlockType.Filled)
            {
                light2D.enabled = true;
                spriteRenderer.sprite = filledSprite;
            }
            else if (blockType == BlockType.Prohibited)
            {
                light2D.enabled = false;
                spriteRenderer.sprite = prohibitedSprite;
            }
            else if (blockType == BlockType.Enter)
            {
                light2D.enabled = false;
                spriteRenderer.sprite = enterSprite;
            }
            else if (blockType == BlockType.Exit)
            {
                light2D.enabled = false;
                spriteRenderer.sprite = exitSprite;
            }
            else
            {
                light2D.enabled = false;
                spriteRenderer.sprite = emptySprite;
            }
        }
#endif
    }

    public enum BlockType
    {
        Empty,
        Filled,
        Prohibited,
        Enter,
        Exit,
    }
}
