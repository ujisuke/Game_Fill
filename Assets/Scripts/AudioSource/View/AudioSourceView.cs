using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Audio;

namespace Assets.Scripts.AudioSource.View
{
    public class AudioSourceView : MonoBehaviour
    {
        private static AudioSourceView instance;
        [SerializeField] private AudioMixer audioMixer;
        [SerializeField] private UnityEngine.AudioSource bgmSource;
        [SerializeField] private AudioClip titleBGM;
        [SerializeField] private AudioClip mapBGM;
        [SerializeField] private AudioClip farceBGM;
        [SerializeField] private AudioClip endingBGM;
        [SerializeField] private List<AudioClip> stageBGMList;
        [SerializeField] private float fadeSeconds;
        [SerializeField] private UnityEngine.AudioSource closeSESource;
        [SerializeField] private UnityEngine.AudioSource fillSESource;
        [SerializeField] private UnityEngine.AudioSource deadSESource;
        [SerializeField] private UnityEngine.AudioSource goodSESource;
        [SerializeField] private UnityEngine.AudioSource clearSESource;
        [SerializeField] private UnityEngine.AudioSource timeLimitSESource;
        [SerializeField] private UnityEngine.AudioSource slowSESource;
        [SerializeField] private UnityEngine.AudioSource turnSESource;
        [SerializeField] private UnityEngine.AudioSource selectSESource;
        [SerializeField] private UnityEngine.AudioSource chooseSESource;
        [SerializeField] private UnityEngine.AudioSource textSESource;
        private readonly List<string> volumeNameList = new() { "MASTER", "BGM", "SE" };
        private int currentStageNumber;

        public static AudioSourceView Instance => instance;
        public List<string> VolumeNameList => volumeNameList;

        private void Awake()
        {
            currentStageNumber = -1;
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            for (int i = 0; i < volumeNameList.Count; i++)
                UpdateVolume(i, 0, out _);
        }

        public int GetVolumeLinear(int index)
        {
            return ES3.Load(volumeNameList[index], 50);
        }

        private float ConvertLinearToDecibel(int linear)
        {
            if (linear == 0)
                return -80;
            return 20 * Mathf.Log10(linear * 0.01f * 2);
        }

        public void UpdateVolume(int index, int addValue, out int updatedVolumeLinear)
        {
            int volumeLinear = ES3.Load(volumeNameList[index], 50);
            volumeLinear = math.clamp(volumeLinear + addValue, 0, 100);
            audioMixer.SetFloat(volumeNameList[index], ConvertLinearToDecibel(volumeLinear));
            ES3.Save(volumeNameList[index], volumeLinear);
            updatedVolumeLinear = volumeLinear;
        }

        public void PlayStageBGM(int stageNumber)
        {
            if (stageNumber < 0 || stageNumber >= stageBGMList.Count || currentStageNumber == stageNumber)
                return;

            bgmSource.clip = stageBGMList[stageNumber];
            bgmSource.Play();
            currentStageNumber = stageNumber;
        }

        public void PlayTitleBGM()
        {
            bgmSource.clip = titleBGM;
            bgmSource.Play();
            currentStageNumber = -1;
        }

        public void PlayMapBGM()
        {
            bgmSource.clip = mapBGM;
            bgmSource.Play();
            currentStageNumber = -1;
        }

        public void PlayFarceBGM()
        {
            bgmSource.clip = farceBGM;
            bgmSource.Play();
            currentStageNumber = -1;
        }

        public void PlayEndingBGM()
        {
            bgmSource.clip = endingBGM;
            bgmSource.Play();
            currentStageNumber = -1;
        }

        public async UniTask FadeOutBGM()
        {
            var token = this.GetCancellationTokenOnDestroy();
            float initVolume = bgmSource.volume;
            var initClip = bgmSource.clip;

            for (int i = 0; i < 100; i++)
            {
                bgmSource.volume = Mathf.Lerp(initVolume, 0, i * 0.01f);
                await UniTask.Delay(TimeSpan.FromSeconds(fadeSeconds * 0.01f), cancellationToken: token);
            }

            if (bgmSource.clip == initClip)
                bgmSource.Stop();
            bgmSource.volume = initVolume;
        }

        public void PlayCloseSE()
        {
            closeSESource.PlayOneShot(closeSESource.clip);
        }

        public void PlayFillSE()
        {
            fillSESource.Play();
        }

        public void StopFillSE()
        {
            fillSESource.Stop();
        }

        public void PlayDeadSE()
        {
            deadSESource.PlayOneShot(deadSESource.clip);
        }

        public void PlayGoodSE()
        {
            goodSESource.PlayOneShot(goodSESource.clip);
        }

        public void PlayClearSE()
        {
            clearSESource.PlayOneShot(clearSESource.clip);
        }

        public void PlayTimeLimitSE()
        {
            timeLimitSESource.PlayOneShot(timeLimitSESource.clip);
        }

        public void PlaySlowSE()
        {
            slowSESource.PlayOneShot(slowSESource.clip);
        }

        public void PlayTurnSE()
        {
            turnSESource.PlayOneShot(turnSESource.clip);
        }

        public void PlaySelectSE()
        {
            selectSESource.PlayOneShot(selectSESource.clip);
        }

        public void PlayChooseSE()
        {
            chooseSESource.PlayOneShot(chooseSESource.clip);
        }

        public void PlayTextSE()
        {
            textSESource.PlayOneShot(textSESource.clip);
        }
    }
}
