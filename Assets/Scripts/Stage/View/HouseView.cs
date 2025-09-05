using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Assets.Scripts.Stage.View
{
    public class HouseView : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Light2D light2D;

        public void Break()
        {
            animator.Play("Break");
        }

        public void Illuminate()
        {
            light2D.enabled = true;
            animator.Play("Illuminate");
        }
    }
}
