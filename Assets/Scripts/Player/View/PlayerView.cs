using Assets.Scripts.Common.View;
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
        private GameObject hurtBoxObject;

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

        public void DestroyHurtBox()
        {
            Destroy(hurtBoxObject);
        }
    }
}
