using Assets.Scripts.Common.Data;
using Unity.Mathematics;

namespace Assets.Scripts.Map.Model
{
    public class MapModel
    {
        private readonly SceneNameData sceneNameData;
        private static int currentStageIndex = 0;
        private static bool isEasyMode = false;
        private static bool isHardMode = false;
        public static int CurrentStageIndex => currentStageIndex;
        public static int StageIndexUpper => math.min(ES3.Load("ClearedStageIndex", -1) + 1, 5);
        public static bool IsEasyMode => isEasyMode;
        public static bool IsHardMode => isHardMode;
        public string CurrentStageName => sceneNameData.GetStageName(currentStageIndex);
        public bool IsStageIndexUpper => currentStageIndex == StageIndexUpper;
        public bool IsStageIndexLower => currentStageIndex == 0;

        public MapModel(SceneNameData sceneNameData)
        {
            this.sceneNameData = sceneNameData;
        }

        public void UpdateStageIndex(int additionalIndex, out bool isChanged)
        {
            int newStageIndex = math.clamp(currentStageIndex + additionalIndex, 0, StageIndexUpper);
            isChanged = newStageIndex != currentStageIndex;
            currentStageIndex = newStageIndex;
        }

        public static void SetDifficulty(bool isUp)
        {
            if (isEasyMode && isUp)
            {
                isEasyMode = false;
                isHardMode = false;
                return;
            }
            else if (!isEasyMode && !isHardMode && isUp)
            {
                isEasyMode = false;
                isHardMode = true;
                return;
            }
            else if (isHardMode && !isUp)
            {
                isEasyMode = false;
                isHardMode = false;
                return;
            }
            else if (!isEasyMode && !isHardMode && !isUp)
            {
                isEasyMode = true;
                isHardMode = false;
                return;
            }
        }

        public static void ResetDifficulty()
        {
            isEasyMode = false;
            isHardMode = false;
        }
    }
}
