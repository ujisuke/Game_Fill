using UnityEngine;

namespace Assets.Scripts.Stage.Data
{
    [CreateAssetMenu(fileName = "BlockData", menuName = "ScriptableObjects/BlockData")]
    public class BlockData : ScriptableObject
    {
        [SerializeField] private float becomeWallSeconds;
        [SerializeField] private float filledAnimSeconds;

        public float BecomeWallSeconds => becomeWallSeconds;
        public float FilledAnimSeconds => filledAnimSeconds;
    }
}
