using Assets.Scripts.Common.Controller;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Volume.Controller
{
    public class VolumeStateInitial : IVolumeState
    {
        private readonly VolumeController vC;
        private readonly VolumeStateMachine vSM;
        private int selectedIndex;

        public VolumeStateInitial(VolumeController vC, VolumeStateMachine vSM)
        {
            this.vC = vC;
            this.vSM = vSM;
        }

        public void OnStateEnter()
        {
            selectedIndex = 0;
            vC.UpdateVolumeButtonSelection(selectedIndex);
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetLeftKeyWithCooldown())
            {
                selectedIndex = math.max(0, selectedIndex - 1);
                vC.UpdateVolumeButtonSelection(selectedIndex);
            }
            else if (CustomInputSystem.Instance.GetRightKeyWithCooldown())
            {
                selectedIndex = math.min(3, selectedIndex + 1);
                vC.UpdateVolumeButtonSelection(selectedIndex);
            }

            if (CustomInputSystem.Instance.GetPauseKeyWithCooldown()
            || (CustomInputSystem.Instance.DoesSelectKeyUp() && selectedIndex == 3))
                vSM.ChangeState(new VolumeStateExitPage());
        }

        public void OnStateExit()
        {

        }
    }
}
