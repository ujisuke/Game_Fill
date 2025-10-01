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
            //Imageがダミーのスプライトと同じになるまでにタイムラグがあり，最初に見た目がチラついていた
            //その対策として，最初だけスプライトを直に適用している
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

        public void SetVolumeText(string newText)
        {
            text.text = newText;
        }
    }
}
