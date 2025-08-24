using Assets.Scripts.Stage.Model;
using Assets.Scripts.Stage.View;
using UnityEngine;

namespace Assets.Scripts.Stage.Controller
{
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private BlockView blockView;
        private BlockModel blockModel;
        public BlockModel BlockModel => blockModel;

        public void Initialize()
        {
            bool isWallInitial = blockView.IsWallInitial;
            blockModel = new BlockModel(this, transform.position, transform.localScale, isWallInitial);
            blockView.InstantiateHitBox(blockModel.HitBox);
            blockView.SetHitBoxActive(isWallInitial);
            if (isWallInitial)
                PlayAnim("Wall");
            else
                PlayAnim("Empty");
        }

        public void Fill()
        {
            blockView.SetHitBoxActive(true);
        }

        public void PlayAnim(string animName, float speed = 1f)
        {
            blockView.PlayAnim(animName, speed);
        }
    }
}
