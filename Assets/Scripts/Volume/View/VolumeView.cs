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


        private int currentIndex;

        private void Awake()
        {
            currentIndex = 0;
            buttonList[0].PlayAnim("Selected");
            rightArrowList[0].PlayAnim("Awake");
            leftArrowList[0].PlayAnim("Awake");
            for(int i = 0; i < volumeNames.Count; i++)
            {
                audioMixer.GetFloat(volumeNames[i], out var volume);
                buttonList[i].SetVolumeText(volumeNames[i] + " " + (volume + 80).ToString("F0") + "%");
            }
        }

        public void UpdateVolumeButtonSelection(int index)
        {
            if (index == currentIndex)
                return;

            if (index == buttonList.Count - 1)
            {
                buttonList[currentIndex].PlayAnim("Deselected");
                rightArrowList[currentIndex].PlayAnim("Empty");
                leftArrowList[currentIndex].PlayAnim("Empty");
                buttonList[index].PlaySelectedAnim();
            }
            else if (currentIndex == buttonList.Count - 1)
            {
                buttonList[currentIndex].PlayDeselectedAnim();
                buttonList[index].PlayAnim("Selected");
                rightArrowList[index].PlayAnim("Awake");
                leftArrowList[index].PlayAnim("Awake");
            }
            else
            {
                rightArrowList[currentIndex].PlayAnim("Empty");
                leftArrowList[currentIndex].PlayAnim("Empty");
                buttonList[currentIndex].PlayAnim("Deselected");
                buttonList[index].PlayAnim("Selected");
                rightArrowList[index].PlayAnim("Awake");
                leftArrowList[index].PlayAnim("Awake");
            }

            currentIndex = index;
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
