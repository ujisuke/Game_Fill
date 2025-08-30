using UnityEngine;

namespace Assets.Scripts.Common.Data
{
    [CreateAssetMenu(fileName = "ViewData", menuName = "ScriptableObjects/ViewData")]
    public class ViewData : ScriptableObject
    {
        [SerializeField] private float blockBecomeWallSeconds;
        [SerializeField] private float blockFilledAnimSeconds;

        [SerializeField] private float slowInAnimSeconds;
        [SerializeField] private float goodAnimSeconds;
        [SerializeField] private float clearAnimDelaySeconds;
        [SerializeField] private float clearAnimSeconds;
        [SerializeField] private float paintStageDelaySeconds;
        [SerializeField] private float clearTextDelaySeconds;
        [SerializeField] private float clearTextAnimSeconds;
        [SerializeField] private float clearFinalAnimDelaySeconds;
        [SerializeField] private float closeAnimDelaySeconds;
        [SerializeField] private float closeAnimSeconds;
        [SerializeField] private float loadSceneDelaySeconds;
        [SerializeField] private float openAnimSeconds;

        public float BlockBecomeWallSeconds => blockBecomeWallSeconds;
        public float BlockFilledAnimSeconds => blockFilledAnimSeconds;
        public float SlowInAnimSeconds => slowInAnimSeconds;
        public float GoodAnimSeconds => goodAnimSeconds;
        public float ClearAnimDelaySeconds => clearAnimDelaySeconds;
        public float ClearAnimSeconds => clearAnimSeconds;
        public float PaintStageDelaySeconds => paintStageDelaySeconds;
        public float ClearTextDelaySeconds => clearTextDelaySeconds;
        public float ClearTextAnimSeconds => clearTextAnimSeconds;
        public float ClearFinalAnimDelaySeconds => clearFinalAnimDelaySeconds;
        public float CloseAnimDelaySeconds => closeAnimDelaySeconds;
        public float CloseAnimSeconds => closeAnimSeconds;
        public float LoadSceneDelaySeconds => loadSceneDelaySeconds;
        public float OpenAnimSeconds => openAnimSeconds;
    }
}
