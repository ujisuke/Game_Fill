using UnityEngine;

namespace Assets.Scripts.Player.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private Vector2 viewScale;

        public float MoveSpeed => moveSpeed;
        public Vector2 HurtBoxScale => hurtBoxScale;
        public Vector2 ViewScale => viewScale;
    }
}
