using System.Collections.Generic;
using Assets.Scripts.Common.View;
using UnityEngine;

namespace Assets.Scripts.Pause.View
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private List<ImageView> initButtonList;
        [SerializeField] private List<ImageView> volumeButtonList;
        [SerializeField] private Canvas volumeCanvas;

        private void Awake()
        {
            initButtonList[0].PlayAnim("Selected");
            for (int i = 1; i < initButtonList.Count; i++)
                initButtonList[i].PlayAnim("DeSelected");
        }

        public void UpdateInitButtonSelection(int index)
        {
            for (int i = 0; i < initButtonList.Count; i++)
            {
                if (i == index)
                    initButtonList[i].PlayAnim("Selected");
                else
                    initButtonList[i].PlayAnim("DeSelected");
            }
        }

        public void UpdateVolumeButtonSelection(int index)
        {
            for (int i = 0; i < volumeButtonList.Count; i++)
            {
                if (i == index)
                    volumeButtonList[i].PlayAnim("Selected");
                else
                    volumeButtonList[i].PlayAnim("DeSelected");
            }
        }

        public void SetActiveVolumePage(bool isActive)
        {
            volumeCanvas.gameObject.SetActive(isActive);
        }
    }
}
