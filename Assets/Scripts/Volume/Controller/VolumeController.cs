using System.Threading;
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

        public void UpdateVolumeButtonSelection(int index)
        {
            volumeView.UpdateVolumeButtonSelection(index);
        }

        public void SelectRight(int index, CancellationToken token)
        {
            volumeView.SelectRight(index, token).Forget();
        }

        public void SelectLeft(int index, CancellationToken token)
        {
            volumeView.SelectLeft(index, token).Forget();
        }
    }
}
