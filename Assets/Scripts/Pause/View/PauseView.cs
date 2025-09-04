using System.Collections.Generic;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Pause.View
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private List<ButtonView> buttonList;
        private int currentIndex;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            currentIndex = 0;
            buttonList[currentIndex].PlaySelectedAnim();
        }

        public void UpdateInitButtonSelection(int index)
        {
            if (currentIndex == index) return;

            buttonList[currentIndex].PlayDeselectedAnim();
            currentIndex = index;
            buttonList[currentIndex].PlaySelectedAnim();
        }
    }
}
