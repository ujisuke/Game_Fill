using Assets.Scripts.Map.Data;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Gallery.Model
{
    public class GalleryModel
    {
        private readonly SceneNameData sceneNameData;
        private static int currentStageIndex = 0;
        private static int currentStageChildIndex = 0;
        private static bool isHardMode = false;
        public static int CurrentStageIndex => currentStageIndex;
        public static int CurrentStageChildIndex => currentStageChildIndex;
        public static bool IsHardMode => isHardMode;
        public string CurrentStageName => $"{currentStageIndex}-{currentStageChildIndex + 1}";
        public static int StageChildIndexUpper => 4;
        public static int StageIndexUpper => ES3.Load("ClearedStageIndex", -1);
        public static bool IsStageIndexUpper => currentStageIndex == StageIndexUpper;
        public static bool IsStageIndexLower => currentStageIndex == 0;

        public GalleryModel(SceneNameData sceneNameData)
        {
            this.sceneNameData = sceneNameData;
        }

        public void UpdateStageAndChildIndex(int additionalIndex, out bool isChildIndexChanged, out bool isStageIndexChanged)
        {
            int newStageChildIndex = math.clamp(currentStageChildIndex + additionalIndex, 0, StageChildIndexUpper);

            if (newStageChildIndex != currentStageChildIndex)
            {
                isChildIndexChanged = true;
                isStageIndexChanged = false;
                currentStageChildIndex = newStageChildIndex;
                return;
            }

            int newStageIndex = math.clamp(currentStageIndex + additionalIndex, 0, StageIndexUpper);
            isStageIndexChanged = newStageIndex != currentStageIndex;
            currentStageIndex = newStageIndex;

            if (!isStageIndexChanged)
            {
                isChildIndexChanged = false;
                return;
            }

            if (additionalIndex > 0)
                currentStageChildIndex = 0;
            else if (additionalIndex < 0)
                currentStageChildIndex = StageChildIndexUpper;
            isChildIndexChanged = true;
        }

        public static void SetDifficulty(bool isHard)
        {
            isHardMode = isHard;
        }
        
        public void SetCurrentStageAndChildIndexFromTitle()
        {
            currentStageChildIndex = 0;
            currentStageIndex = ES3.Load("ClearedStageIndex", -1) < 0 ? -1 : 0;
        }
    }
}
