using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Pause.View;
using UnityEngine;

namespace Assets.Scripts.Pause.Controller
{
    public class PauseController : MonoBehaviour
    {
        [SerializeField] private PauseView pauseView;
        [SerializeField] private SceneNameData sceneNameData;
        private PauseStateMachine pSM;
        public string VolumeSceneName => sceneNameData.VolumeSceneName;
        public string PauseSceneName => sceneNameData.PauseSceneName;

        private void Awake()
        {
            AudioSourceView.Instance.PlaySelectSE();
            pSM = new PauseStateMachine(this);
        }

        private void Update()
        {
            pSM.HandleInput();
        }

        public void UpdateInitButtonSelection(int indexNew, int indexPrev, bool isFromSetVolume = false)
        {
            if (indexNew == indexPrev && !isFromSetVolume)
                return;
            if (!isFromSetVolume)  //音量設定画面から戻るときは「戻る」ボタン選択のSEと被るため鳴らさない
                AudioSourceView.Instance.PlaySelectSE();
            pauseView.UpdateInitButtonSelection(indexNew, indexPrev);
        }
    }
}
