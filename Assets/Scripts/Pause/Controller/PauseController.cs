using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Controller;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Pause.View;
using Assets.Scripts.Stage.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (!isFromSetVolume)
                AudioSourceView.Instance.PlaySelectSE();
            pauseView.UpdateInitButtonSelection(indexNew, indexPrev);
        }
    }
}
