using Assets.Scripts.Volume.View;
using UnityEngine;

namespace Assets.Scripts.Volume.Controller
{
    public class VolumeController : MonoBehaviour
    {
        [SerializeField] private VolumeView volumeView;
        private VolumeStateMachine vSM;

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
    }
}
