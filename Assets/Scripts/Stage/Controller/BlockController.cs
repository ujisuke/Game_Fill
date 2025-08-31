using Assets.Scripts.Stage.Model;
using Assets.Scripts.Stage.View;
using UnityEngine;
using Assets.Scripts.Common.Data;

namespace Assets.Scripts.Stage.Controller
{
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private BlockView blockView;
        [SerializeField] private ViewData viewData;

        private BlockModel blockModel;
        public BlockModel BlockModel => blockModel;

        public void Initialize()
        {
            bool isWallInitial = blockView.IsWallInitial;
            bool canBeFilled = blockView.CanBeFilled;
            bool isExit = blockView.IsExit;
            blockModel = new BlockModel(this, transform.position, transform.localScale, isWallInitial, canBeFilled, isExit, viewData);
            blockView.InstantiateHitBox(blockModel.HitBox);
            blockView.SetHitBoxActive(isWallInitial);
            blockView.SetInitialAnim();
        }

        public void Fill()
        {
            blockView.SetHitBoxActive(true);
        }

        public void PlayAnim(string animName, float speed = 1f)
        {
            blockView.PlayAnim(animName, speed);
        }

        public void SetAnimSpeed(float speed)
        {
            blockView.SetAnimSpeed(speed);
        }
    }
}
