using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Title.View;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Localization.Settings;

namespace Assets.Scripts.Title.Controller
{
    public class TitleController : MonoBehaviour
    {
        [SerializeField] private TitleView titleView;
        [SerializeField] private SceneNameData sceneNameData;
        private TitleStateMachine tSM;
        public string VolumeSceneName => sceneNameData.VolumeSceneName;
        public string MapSceneName => sceneNameData.MapSceneName;
        public string TutorialSceneName => sceneNameData.TutorialSceneName;
        public string GallerySceneName => sceneNameData.GallerySceneName;

        private void Start()
        {
            tSM = new TitleStateMachine(this);
            AudioSourceView.Instance.PlayTitleBGM();
        }

        private void Update()
        {
            tSM.HandleInput();
        }

        public void SetJapanese(bool isJA)
        {
            if(isJA == (LocalizationSettings.SelectedLocale.Identifier.Code == "ja"))
                return;
            AudioSourceView.Instance.PlaySelectSE();
            titleView.SetJapanese(isJA);
        }

        public void UpdateInitButtonSelection(int indexNew, int indexPrev, bool isInit = false)
        {
            if (indexPrev == indexNew && !isInit)
                return;
            AudioSourceView.Instance.PlaySelectSE();
            titleView.UpdateInitButtonSelection(indexNew, indexPrev);
        }

        public void SetActiveButtons(bool isActive)
        {
            titleView.SetActiveButtons(isActive);
        }

        public void OpenScene()
        {
            titleView.Open();
        }

        public async UniTask CloseSceneWithBlack(CancellationToken token)
        {
            await titleView.CloseWithBlack(token);
        }

        public async UniTask CloseSceneToTutorial(CancellationToken token)
        {
            await titleView.CloseToTutorial(token);
        }
    }
}
