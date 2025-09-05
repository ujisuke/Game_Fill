using Assets.Scripts.Map.Data;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Map.Model
{
    public class MapModel
    {
        private readonly SceneNameData sceneNameData;
        private static int currentStageIndex = 0;
        private static bool isHardMode = false;
        public static int CurrentStageIndex => currentStageIndex;
        public static bool IsHardMode => isHardMode;
        public string CurrentStageName => sceneNameData.GetCurrentStageName(currentStageIndex);

        public MapModel(SceneNameData sceneNameData)
        {
            this.sceneNameData = sceneNameData;
        }

        public void UpdateStageIndex(int additionalIndex)
        {
            int listUpper = math.min(ES3.Load("ClearedStageIndex", 0) + 1, sceneNameData.StageCount - 1);
            currentStageIndex = math.clamp(currentStageIndex + additionalIndex, 0, listUpper);
        }

        public static void SetDifficulty(bool isHard)
        {
            isHardMode = isHard;
        }
    }
}
