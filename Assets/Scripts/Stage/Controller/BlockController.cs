using Assets.Scripts.Stage.Model;
using Assets.Scripts.Stage.View;
using UnityEngine;

namespace Assets.Scripts.Stage.Controller
{
    public class BlockController : MonoBehaviour
    {
        [SerializeField] private bool isWallOnFirst;
        [SerializeField] private BlockView blockView;
        private BlockModel blockModel;
        public bool IsWallOnFirst => isWallOnFirst;
        public BlockModel BlockModel => blockModel;

        public void Initialize()
        {
            blockModel = new BlockModel(this, transform.position, transform.localScale, isWallOnFirst);
            blockView.InstantiateHitBox(blockModel.HitBox);
            blockView.SetHitBoxActive(isWallOnFirst);
        }

        private void Update()
        {

        }

        public void Fill()
        {
            blockView.SetHitBoxActive(true);
            blockView.Fill();
        }
    }
}
