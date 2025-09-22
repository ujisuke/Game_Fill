using System.Threading;
using Assets.Scripts.Common.Controller;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Volume.Controller
{
    public class VolumeStateInitial : IVolumeState
    {
        private readonly VolumeController vC;
        private readonly VolumeStateMachine vSM;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;
        private int selectedIndex;

        public VolumeStateInitial(VolumeController vC, VolumeStateMachine vSM)
        {
            this.vC = vC;
            this.vSM = vSM;
            cTS = new();
            token = cTS.Token;
        }

        public void OnStateEnter()
        {
            selectedIndex = 0;
            vC.UpdateVolumeButtonSelection(selectedIndex, selectedIndex, true);
        }

        public void HandleInput()
        {
            if (CustomInputSystem.Instance.GetUpKeyWithCooldown())
            {
                int selectedIndexNew = math.max(0, selectedIndex - 1);
                vC.UpdateVolumeButtonSelection(selectedIndexNew, selectedIndex);
                selectedIndex = selectedIndexNew;
            }
            else if (CustomInputSystem.Instance.GetDownKeyWithCooldown())
            {
                int selectedIndexNew = math.min(3, selectedIndex + 1);
                vC.UpdateVolumeButtonSelection(selectedIndexNew, selectedIndex);
                selectedIndex = selectedIndexNew;
            }
            else if (CustomInputSystem.Instance.GetRightKeyWithCooldown() && selectedIndex != 3)
                vC.SelectRight(selectedIndex, token);
            else if (CustomInputSystem.Instance.GetLeftKeyWithCooldown() && selectedIndex != 3)
                vC.SelectLeft(selectedIndex, token);

            if (CustomInputSystem.Instance.GetPauseKeyWithCooldown()
            || (CustomInputSystem.Instance.GetSelectKeyUp() && selectedIndex == 3))
                vSM.ChangeState(new VolumeStateExitPage(vC));
        }

        public void OnStateExit()
        {
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}
