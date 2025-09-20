using System;
using System.Collections.Generic;
using System.Threading;
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
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private List<string> volumeNames;

        private void Awake()
        {
            buttonList[0].PlayAnim("Selected");
            rightArrowList[0].PlayAnim("Awake");
            leftArrowList[0].PlayAnim("Awake");
            for(int i = 0; i < volumeNames.Count; i++)
            {
                audioMixer.GetFloat(volumeNames[i], out var volume);
                buttonList[i].SetVolumeText(volumeNames[i] + " " + (volume + 80).ToString("F0") + "%");
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
                buttonList[indexPrev].PlayAnim("Deselected");
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
            await UniTask.DelayFrame(1, cancellationToken: token);
            rightArrowList[index].PlayAnim("Selected");
            audioMixer.GetFloat(volumeNames[index], out var volume);
            audioMixer.SetFloat(volumeNames[index], math.clamp(volume + 5, -80, 20));
            audioMixer.GetFloat(volumeNames[index], out var updatedVolume);
            buttonList[index].SetVolumeText(volumeNames[index] + " " + (updatedVolume + 80).ToString("F0") + "%");
        }

        public async UniTask SelectLeft(int index, CancellationToken token)
        {
            leftArrowList[index].PlayAnim("Awake");
            await UniTask.DelayFrame(1, cancellationToken: token);
            leftArrowList[index].PlayAnim("Selected");
            audioMixer.GetFloat(volumeNames[index], out var volume);
            audioMixer.SetFloat(volumeNames[index], math.clamp(volume - 5, -80, 20));
            audioMixer.GetFloat(volumeNames[index], out var updatedVolume);
            buttonList[index].SetVolumeText(volumeNames[index] + " " + (updatedVolume + 80).ToString("F0") + "%");
        }
    }
}
