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
        private int index = 0;
        public string MapSceneName => mapSceneName;
        public string CurrentStageName => stageNameList[index];

        public void UpdateCurrentStageName(bool isGoingToNext)
        {
            index = math.clamp(index + (isGoingToNext ? 1 : -1), 0, stageNameList.Count - 1);
        }
    }
}
