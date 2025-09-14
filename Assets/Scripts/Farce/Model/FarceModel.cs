using Assets.Scripts.Map.Model;
using UnityEngine;

namespace Assets.Scripts.Farce.Model
{
    public class FarceModel
    {
        public static void SaveClearStage()
        {
            int currentStageIndex = MapModel.CurrentStageIndex;
            int clearedStageIndex = ES3.Load("ClearedStageIndex", 0);
            if (currentStageIndex > clearedStageIndex)
                ES3.Save("ClearedStageIndex", currentStageIndex);

            if (MapModel.IsHardMode)
                ES3.Save("Hard" + currentStageIndex, true);
        }
    }
}
