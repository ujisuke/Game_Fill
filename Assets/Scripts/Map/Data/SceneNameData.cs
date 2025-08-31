using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Map.Data
{
    [CreateAssetMenu(fileName = "SceneNameData", menuName = "ScriptableObjects/SceneNameData")]
    public class SceneNameData : ScriptableObject
    {
        [SerializeField] private string titleSceneName;
        [SerializeField] private string mapSceneName;
        [SerializeField] private string pauseSceneName;
        [SerializeField] private string volumeSceneName;
        [SerializeField] private string farceSceneName;
        [SerializeField] private List<string> stageNameList;
        public static int CurrentStageIndex = 0;

        public string TitleSceneName => titleSceneName;
        public string MapSceneName => mapSceneName;
        public string PauseSceneName => pauseSceneName;
        public string CurrentStageName => stageNameList[CurrentStageIndex];
        public string VolumeSceneName => volumeSceneName;
        public string FarceSceneName => farceSceneName;

        public void UpdateCurrentStageName(int additionIndex)
        {
            CurrentStageIndex = math.clamp(CurrentStageIndex + additionIndex, 0, stageNameList.Count - 1);
        }
    }
}
