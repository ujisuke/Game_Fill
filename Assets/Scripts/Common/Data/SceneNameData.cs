using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Common.Data
{
    [CreateAssetMenu(fileName = "SceneNameData", menuName = "ScriptableObjects/SceneNameData")]
    public class SceneNameData : ScriptableObject
    {
        [SerializeField] private string titleSceneName;
        [SerializeField] private string mapSceneName;
        [SerializeField] private string pauseSceneName;
        [SerializeField] private string volumeSceneName;
        [SerializeField] private string endingSceneName;
        [SerializeField] private string gallerySceneName;
        [SerializeField] private List<string> stageNameList;

        public string TitleSceneName => titleSceneName;
        public string MapSceneName => mapSceneName;
        public string PauseSceneName => pauseSceneName;
        public string VolumeSceneName => volumeSceneName;
        public string EndingSceneName => endingSceneName;
        public string GallerySceneName => gallerySceneName;
        public int StageCount => stageNameList.Count;
        public string TutorialSceneName => stageNameList[0];

        public string GetStageName(int index)
        {
            return stageNameList[index];
        }
    }
}
