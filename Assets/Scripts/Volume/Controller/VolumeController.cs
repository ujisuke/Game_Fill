using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Map.Data;
using Assets.Scripts.Volume.View;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Volume.Controller
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private VolumeView volumeView;
        [SerializeField] private SceneNameData sceneNameData;
        private VolumeStateMachine vSM;
        public string VolumeSceneName => sceneNameData.VolumeSceneName;
        public string PauseSceneName => sceneNameData.PauseSceneName;

        private void Awake()
        {
            vSM = new VolumeStateMachine(this);
        }

        private void Update()
        {
            vSM.HandleInput();
        }

        public void UpdateVolumeButtonSelection(int indexNew, int indexPrev, bool isInit = false)
        {
            if (indexNew == indexPrev && !isInit)
                return;
            AudioSourceView.Instance.PlaySelectSE();
            volumeView.UpdateVolumeButtonSelection(indexNew, indexPrev);
        }

        public void SelectRight(int index, CancellationToken token)
        {
            int volumeLinear = AudioSourceView.Instance.GetVolumeLinear(index);
            if (volumeLinear >= 100)
                return;
            AudioSourceView.Instance.PlaySelectSE();
            volumeView.SelectRight(index, token).Forget();
        }

        public void SelectLeft(int index, CancellationToken token)
        {
            int volumeLinear = AudioSourceView.Instance.GetVolumeLinear(index);
            if (volumeLinear <= 0)
                return;
            AudioSourceView.Instance.PlaySelectSE();
            volumeView.SelectLeft(index, token).Forget();
        }
    }
}
