using Assets.Scripts.Common.View;
using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Model;
using UnityEngine;

namespace Assets.Scripts.Player.View
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        private ObjectAnimation objectAnimation;
        [SerializeField] private GameObject hurtBoxPrefab;
        [SerializeField] private PlayerData playerData;
        private GameObject hurtBoxObject;
        private Vector2 initScale;

        private void Awake()
        {
            initScale = spriteRenderer.transform.localScale;
        }

        public void SetPos(Vector2 pos)
        {
            transform.position = pos;
        }

        public void PlayAnim(string animName, float animSeconds)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.Play(animName, animSeconds);
        }

        public void StopAnim()
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.Stop();
        }

        public void FlipX(bool isLeft)
        {
            objectAnimation ??= new ObjectAnimation(animator, spriteRenderer);
            objectAnimation.FlipX(isLeft);
        }

        public void InstantiateHurtBox(HurtBox hurtBox)
        {
            hurtBoxObject = Instantiate(hurtBoxPrefab, hurtBox.Pos, Quaternion.identity);
            hurtBoxObject.transform.localScale = hurtBox.Scale;
        }

        public void SetHurtBoxPos(HurtBox hurtBox)
        {
            hurtBoxObject.transform.position = hurtBox.Pos;
        }

        public void Compress(bool isCompress)
        {
            if (isCompress)
                spriteRenderer.transform.localScale = new Vector2(initScale.x * playerData.Compress.x, initScale.y * playerData.Compress.y);
            else
                spriteRenderer.transform.localScale = initScale;
        }

        public void DestroyHurtBox()
        {
            Destroy(hurtBoxObject);
        }
    }
}
