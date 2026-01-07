using Unity.Mathematics;

namespace Assets.Scripts.Gallery.Model
{
    public class GalleryModel
    {
        //(例)ステージ名1-3に対し，StageIndex=1，StageChildIndex=2
        //StageChildIndexは0から始まり，表示される数より1小さいので注意
        private static int currentStageIndex = 0;
        private static int currentStageChildIndex = 0;
        private static bool isEasyMode = false;
        private static bool isHardMode = false;
        public static int CurrentStageIndex => currentStageIndex;
        public static int CurrentStageChildIndex => currentStageChildIndex;
        public static bool IsEasyMode => isEasyMode;
        public static bool IsHardMode => isHardMode;
        public string CurrentStageName => $"{currentStageIndex}-{currentStageChildIndex + 1}";
        public static int StageChildIndexUpper => 4;
        public static int StageIndexUpper => ES3.Load("ClearedStageIndex", -1);
        public static bool IsStageIndexUpper => currentStageIndex == StageIndexUpper;
        public static bool IsStageIndexLower => currentStageIndex == 0;

        public GalleryModel()
        {

        }

        public void UpdateStageAndChildIndex(int additionalIndex, out bool isChildIndexChanged, out bool isStageIndexChanged)
        {
            int newStageChildIndex = math.clamp(currentStageChildIndex + additionalIndex, 0, StageChildIndexUpper);
            
            //(例)1-1から1-5までの間で選択しているときに，左右キーで1-2や1-4に移動する場合(他のステージに移動しない場合)
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
        
        public void SetCurrentStageAndChildIndexFromTitle()
        {
            currentStageChildIndex = 0;
            currentStageIndex = ES3.Load("ClearedStageIndex", -1) < 0 ? -1 : 0;
        }
    }
}
