using System.Collections.Generic;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Assets.Scripts.Stage.Controller;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Pause.View
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private List<ButtonView> buttonList;
        [SerializeField] private ButtonView galleryButton;

        private void Awake()
        {
            if (StageController.IsInGallery)
            {
                buttonList[2].gameObject.SetActive(false);
                buttonList[2] = galleryButton;
                galleryButton.gameObject.SetActive(true);
            }
            else
                galleryButton.gameObject.SetActive(false);
        }

        public void UpdateInitButtonSelection(int indexNew, int indexPrev)
        {
            buttonList[indexPrev].PlayDeselectedAnim();
            buttonList[indexNew].PlaySelectedAnim();
        }
    }
}
