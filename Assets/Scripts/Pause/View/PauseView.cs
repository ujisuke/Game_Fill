using System.Collections.Generic;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Pause.View
{
    public class PauseView : MonoBehaviour
    {
        [SerializeField] private List<ImageView> initButtonList;
        [SerializeField] private Image back;
        [SerializeField] private ViewData viewData;

        private void Awake()
        {
            initButtonList[0].PlayAnim("Selected");
            for (int i = 1; i < initButtonList.Count; i++)
                initButtonList[i].PlayAnim("DeSelected");

            back.color = viewData.PauseBackColor;
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

        public void SetActiveButtons(bool isActive)
        {
            foreach (var button in initButtonList)
                button.gameObject.SetActive(isActive);
        }
    }
}
