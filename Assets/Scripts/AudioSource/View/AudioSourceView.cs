using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.AudioSource.View
{
    public class AudioSourceView : MonoBehaviour
    {
        private static AudioSourceView instance;
        [SerializeField] private UnityEngine.AudioSource bgmSource;
        [SerializeField] private AudioClip titleBGM;
        [SerializeField] private AudioClip mapBGM;
        [SerializeField] private AudioClip farceBGM;
        [SerializeField] private AudioClip endingBGM;
        [SerializeField] private List<AudioClip> stageBGMList;
        [SerializeField] private float fadeSeconds;
        [SerializeField] private GameObject closeSESource;
        [SerializeField] private UnityEngine.AudioSource fillSESource;
        [SerializeField] private GameObject deadSESource;
        [SerializeField] private GameObject goodSESource;
        [SerializeField] private GameObject clearSESource;
        [SerializeField] private GameObject timeLimitSESource;
        [SerializeField] private GameObject slowSESource;
        [SerializeField] private GameObject turnSESource;
        [SerializeField] private GameObject selectSESource;
        [SerializeField] private GameObject chooseSESource;
        [SerializeField] private GameObject textSESource;

        private int currentStageNumber;

        public static AudioSourceView Instance => instance;

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
            currentStageNumber = -1;
        }

        public void PlayCloseSE()
        {
            Instantiate(closeSESource);
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
            Instantiate(deadSESource);
        }

        public void PlayGoodSE()
        {
            Instantiate(goodSESource);
        }

        public void PlayClearSE()
        {
            Instantiate(clearSESource);
        }

        public void PlayTimeLimitSE()
        {
            Instantiate(timeLimitSESource);
        }

        public void PlaySlowSE()
        {
            Instantiate(slowSESource);
        }

        public void PlayTurnSE()
        {
            Instantiate(turnSESource);
        }

        public void PlaySelectSE()
        {
            Instantiate(selectSESource);
        }

        public void PlayChooseSE()
        {
            Instantiate(chooseSESource);
        }

        public void PlayTextSE()
        {
            Instantiate(textSESource);
        }
    }
}
