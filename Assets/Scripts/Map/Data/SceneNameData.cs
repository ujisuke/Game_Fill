using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Map.Data
{
    [CreateAssetMenu(fileName = "SceneNameData", menuName = "ScriptableObjects/SceneNameData")]
    public class SceneNameData : ScriptableObject
    {
        [SerializeField] private string mapSceneName;
        [SerializeField] private List<string> stageNameList;
        public static int CurrentStageIndex = 0;
        public string MapSceneName => mapSceneName;
        public string CurrentStageName => stageNameList[CurrentStageIndex];

        public void UpdateCurrentStageName(int additionIndex)
        {
            CurrentStageIndex = math.clamp(CurrentStageIndex + additionIndex, 0, stageNameList.Count - 1);
        }
    }
}
