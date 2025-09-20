using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Assets.Scripts.Volume.View
{
    public class VolumeView : MonoBehaviour
    {
        [SerializeField] private List<ButtonView> buttonList;
        [SerializeField] private List<ImageView> rightArrowList;
        [SerializeField] private List<ImageView> leftArrowList;

        private void Awake()
        {
            buttonList[0].PlayAnim("Selected");
            rightArrowList[0].PlayAnim("Awake");
            leftArrowList[0].PlayAnim("Awake");
            for (int i = 0; i < AudioSourceView.Instance.VolumeNameList.Count; i++)
            {
                int volumeLinear = ES3.Load(AudioSourceView.Instance.VolumeNameList[i], 50);
                buttonList[i].SetVolumeText(AudioSourceView.Instance.VolumeNameList[i] + " " + volumeLinear.ToString("F0") + "%");
            }
        }

        public void UpdateVolumeButtonSelection(int indexNew, int indexPrev)
        {
            if (indexNew == buttonList.Count - 1)
            {
                buttonList[indexPrev].PlayAnim("Deselected");
                rightArrowList[indexPrev].PlayAnim("Empty");
                leftArrowList[indexPrev].PlayAnim("Empty");
                buttonList[indexNew].PlaySelectedAnim();
            }
            else if (indexPrev == buttonList.Count - 1)
            {
                buttonList[indexPrev].PlayDeselectedAnim();
                buttonList[indexNew].PlayAnim("Selected");
                rightArrowList[indexNew].PlayAnim("Awake");
                leftArrowList[indexNew].PlayAnim("Awake");
            }
            else
            {
                rightArrowList[indexPrev].PlayAnim("Empty");
                leftArrowList[indexPrev].PlayAnim("Empty");
                buttonList[indexPrev].PlayAnim("Deselected");
                buttonList[indexNew].PlayAnim("Selected");
                rightArrowList[indexNew].PlayAnim("Awake");
                leftArrowList[indexNew].PlayAnim("Awake");
            }
        }

        public async UniTask SelectRight(int index, CancellationToken token)
        {
            rightArrowList[index].PlayAnim("Awake");
            leftArrowList[index].PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            rightArrowList[index].PlayAnim("Selected");

            AudioSourceView.Instance.UpdateVolume(index, 5, out int volumeLinear);
            buttonList[index].SetVolumeText(AudioSourceView.Instance.VolumeNameList[index] + " " + volumeLinear.ToString("F0") + "%");
        }

        public async UniTask SelectLeft(int index, CancellationToken token)
        {
            rightArrowList[index].PlayAnim("Awake");
            leftArrowList[index].PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            leftArrowList[index].PlayAnim("Selected");

            AudioSourceView.Instance.UpdateVolume(index, -5, out int volumeLinear);
            buttonList[index].SetVolumeText(AudioSourceView.Instance.VolumeNameList[index] + " " + volumeLinear.ToString("F0") + "%");
        }
    }
}
