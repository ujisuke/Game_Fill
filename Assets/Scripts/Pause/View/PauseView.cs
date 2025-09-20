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

        public void UpdateInitButtonSelection(int indexNew, int indexPrev)
        {
            buttonList[indexPrev].PlayDeselectedAnim();
            buttonList[indexNew].PlaySelectedAnim();
        }
    }
}
