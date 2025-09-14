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
        [SerializeField] private string endingSceneName;
        [SerializeField] private List<string> stageNameList;

        public string TitleSceneName => titleSceneName;
        public string MapSceneName => mapSceneName;
        public string PauseSceneName => pauseSceneName;
        public string VolumeSceneName => volumeSceneName;
        public string EndingSceneName => endingSceneName;
        public int StageCount => stageNameList.Count;
        public string TutorialSceneName => stageNameList[0];

        public string GetCurrentStageName(int index)
        {
            return stageNameList[index];
        }
    }
}
