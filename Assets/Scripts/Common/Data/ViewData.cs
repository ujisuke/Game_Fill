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
        [SerializeField] private float inBlackAnimSeconds;
        [SerializeField] private float loadSceneDelaySeconds;
        [SerializeField] private float playCloseSEDelaySeconds;
        [SerializeField] private float loadSceneWithBlackDelaySeconds;
        [SerializeField] private float openAnimSeconds;
        [SerializeField] private float outBlackAnimSeconds;
        [SerializeField] private float showCharSeconds;
        [SerializeField] private float showSentenceSeconds;
        [SerializeField] private float houseIlluminateDelaySeconds;
        [SerializeField] private float thanksDelaySeconds;
        [SerializeField] private float loadTitleOnEndingDelaySeconds;
        [SerializeField] private float loadTutorialDelaySeconds;
        [SerializeField] private float loadFarceDelaySeconds;

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
        public float InBlackAnimSeconds => inBlackAnimSeconds;
        public float LoadSceneDelaySeconds => loadSceneDelaySeconds;
        public float PlayCloseSEDelaySeconds => playCloseSEDelaySeconds;
        public float LoadSceneWithBlackDelaySeconds => loadSceneWithBlackDelaySeconds;
        public float OpenAnimSeconds => openAnimSeconds;
        public float OutBlackAnimSeconds => outBlackAnimSeconds;
        public float ShowCharSeconds => showCharSeconds;
        public float ShowSentenceSeconds => showSentenceSeconds;
        public float HouseIlluminateDelaySeconds => houseIlluminateDelaySeconds;
        public float ThanksDelaySeconds => thanksDelaySeconds;
        public float LoadTitleOnEndingDelaySeconds => loadTitleOnEndingDelaySeconds;
        public float LoadTutorialDelaySeconds => loadTutorialDelaySeconds;
        public float LoadFarceDelaySeconds => loadFarceDelaySeconds;

        [SerializeField] private Color slowEffectColor;
        [SerializeField] private Color hardModeColor;

        public Color SlowEffectColor => slowEffectColor;
        public Color HardModeColor => hardModeColor;
    }
}
