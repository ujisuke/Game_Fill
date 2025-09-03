using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Common.View
{
    public class ButtonView : ImageView
    {
        [SerializeField] private Text text;
        [SerializeField] private Sprite selectedInitSprite;

        public void PlaySelectedAnim()
        {
            dummySpriteRenderer.sprite = selectedInitSprite;
            text.enabled = true;
            Update();
            PlayAnim("Selected");
        }

        public void PlayDeselectedAnim()
        {
            text.enabled = false;
            PlayAnim("Deselected");
        }
    }
}
