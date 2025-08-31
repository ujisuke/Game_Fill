using System.Collections.Generic;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Volume.View
{
    public class VolumeView : MonoBehaviour
    {
        [SerializeField] private List<ImageView> volumeButtonList;
        [SerializeField] private ViewData viewData;

        private void Awake()
        {
            volumeButtonList[0].PlayAnim("Selected");
            for (int i = 1; i < volumeButtonList.Count; i++)
                volumeButtonList[i].PlayAnim("DeSelected");
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
    }
}
