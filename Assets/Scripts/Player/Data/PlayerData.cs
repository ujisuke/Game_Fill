using UnityEngine;

namespace Assets.Scripts.Player.Data
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private float stopSeconds;
        [SerializeField] private Vector2 hurtBoxScale;
        [SerializeField] private Vector2 compress;

        public float MoveSpeed => moveSpeed;
        public float StopSeconds => stopSeconds;
        public Vector2 HurtBoxScale => hurtBoxScale;
        public Vector2 Compress => compress;
    }
}
