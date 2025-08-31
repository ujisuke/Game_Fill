using Assets.Scripts.Common.Controller;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Volume.Controller
{
    public class VolumeStateExitPage : IVolumeState
    {
        private static bool doesExit;
        public static bool DoesExit => doesExit;

        public VolumeStateExitPage()
        {

        }

        public void OnStateEnter()
        {
            doesExit = true;
        }

        public void HandleInput()
        {

        }

        public void OnStateExit()
        {

        }

        public static void ResetFlag()
        {
            doesExit = false;
        }
    }
}
