using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.AudioSource.View;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
using Assets.Scripts.Map.Model;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Assets.Scripts.Stage.View
{
    public class StageView : MonoBehaviour
    {
        [SerializeField] private ImageView slowEffectView;
        [SerializeField] private ImageView fillEffectView;
        [SerializeField] private ImageView frontView;
        [SerializeField] private ImageView clearTextView;
        [SerializeField] private ImageView screenView;
        [SerializeField] private ImageView GoodView;
        [SerializeField] private Text timeLimitText;
        [SerializeField] private Sprite frontOpenRellInitSprite;
        [SerializeField] private Sprite frontOpenInitSprite;
        [SerializeField] private ViewData viewData;
        [SerializeField] private List<GameObject> additionalObjectList;
        [SerializeField] private HouseView endingHouse;
        [SerializeField] private TextBox endingThanks;
        private static bool isRetry = false;
        private BlockView[,] blockMap;

        private void Awake()
        {
            slowEffectView.SetColor(viewData.SlowEffectColor);
            if (MapModel.IsHardMode)
                timeLimitText.color = viewData.HardModeColor;
        }

        public void SetBlockMap(Tilemap tilemap)
        {
            Vector2 maxPos = Vector2.zero;
            for (int i = 0; i < tilemap.gameObject.transform.childCount; i++)
            {
                Vector2 pos = tilemap.gameObject.transform.GetChild(i).position;
                maxPos = Vector2.Max(maxPos, pos);
            }
            blockMap = new BlockView[(int)maxPos.x + 1, (int)maxPos.y + 1];
            for (int i = 0; i < tilemap.gameObject.transform.childCount; i++)
            {
                BlockView blockView = tilemap.gameObject.transform.GetChild(i).GetComponent<BlockView>();
                Vector2Int blockIndex = ConvertPosToIndex(blockView.transform.position);
                blockMap[blockIndex.x, blockIndex.y] = blockView;
            }
        }

        private static Vector2Int ConvertPosToIndex(Vector2 pos)
        {
            Vector2 offset = Vector2.zero;
            if (pos.x < 0)
                offset.x = -1;
            if (pos.y < 0)
                offset.y = -1;
            return new Vector2Int((int)pos.x + (int)offset.x, (int)pos.y + (int)offset.y);
        }

        public void PlaySlowEffect()
        {
            if (PlayerModel.Instance == null)
                return;
            slowEffectView.transform.position = PlayerModel.Instance.Pos;
            slowEffectView.PlayAnim("SlowIn", viewData.SlowInAnimSeconds);
        }

        public void StopSlowEffect()
        {
            slowEffectView.PlayAnim("SlowOut");
        }

        public async UniTask PlayClearEffect(CancellationToken token)
        {
            await PlayGoodAndText(token);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.CloseAnimDelaySeconds), cancellationToken: token);
            await CloseStage(token);
        }

        public async UniTask PlayClearFinalEffect(CancellationToken token)
        {
            isRetry = false;
            await PlayGoodAndText(token);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.ClearFinalAnimDelaySeconds), cancellationToken: token);
            fillEffectView.PlayAnim("ClearFinal", viewData.ClearAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.CloseAnimDelaySeconds), cancellationToken: token);
            frontView.PlayAnim("InBlack", viewData.InBlackAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadFarceDelaySeconds), cancellationToken: token);
        }

        public async UniTask PlayEndingEffect(CancellationToken token)
        {
            isRetry = false;
            timeLimitText.enabled = false;
            endingHouse.Break();
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.HouseIlluminateDelaySeconds), cancellationToken: token);
            endingHouse.Illuminate();
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.ThanksDelaySeconds), cancellationToken: token);
            endingThanks.ShowAndPlay();
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadTitleOnEndingDelaySeconds), cancellationToken: token);
        }

        private async UniTask PlayGoodAndText(CancellationToken token)
        {
            StopAllBlocks();
            screenView.PlayAnim("Stop");
            GoodView.transform.position = PlayerModel.Instance.Pos + Vector2.up;
            GoodView.PlayAnim("Good", viewData.GoodAnimSeconds);
            AudioSourceView.Instance.PlayGoodSE();
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.ClearAnimDelaySeconds), cancellationToken: token);
            screenView.PlayAnim("Play");
            GoodView.PlayAnim("Empty");
            timeLimitText.enabled = false;
            for (int i = 0; i < additionalObjectList.Count; i++)
                Destroy(additionalObjectList[i]);
            fillEffectView.PlayAnim("Clear", viewData.ClearAnimSeconds);
            await PaintStage(token);
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.ClearTextDelaySeconds), cancellationToken: token);
            clearTextView.PlayAnim("Awake", viewData.ClearTextAnimSeconds);
            AudioSourceView.Instance.PlayClearSE();
        }

        private async UniTask PaintStage(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.PaintStageDelaySeconds), cancellationToken: token);
            for (int i = 0; i < blockMap.GetLength(0) + blockMap.GetLength(1); i++)
            {
                for (int x = 0; x <= i; x++)
                {
                    int y = i - x;
                    if (x < 0 || x >= blockMap.GetLength(0) || y < 0 || y >= blockMap.GetLength(1))
                        continue;
                    if (blockMap[x, y] == null)
                        continue;
                    blockMap[x, y].PaintColor(Color.black);
                }
                await UniTask.Delay(TimeSpan.FromSeconds(0.01f), cancellationToken: token);
            }
        }

        private void StopAllBlocks()
        {
            foreach (var block in blockMap)
                if (block != null)
                    block.StopAnim();
        }

        public async UniTask CloseStage(CancellationToken token)
        {
            frontView.PlayAnim("Close", viewData.CloseAnimSeconds);
            isRetry = false;
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneDelaySeconds - viewData.PlayCloseSEDelaySeconds), cancellationToken: token);
            AudioSourceView.Instance.PlayCloseSE();
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.PlayCloseSEDelaySeconds), cancellationToken: token);
        }

        public async UniTask CloseStageRetry(CancellationToken token)
        {
            frontView.PlayAnim("CloseRell", viewData.CloseAnimSeconds);
            isRetry = true;
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneDelaySeconds - viewData.PlayCloseSEDelaySeconds), cancellationToken: token);
            AudioSourceView.Instance.PlayCloseSE();
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.PlayCloseSEDelaySeconds), cancellationToken: token);
        }

        public async UniTask CloseStageWithBlack(CancellationToken token)
        {
            frontView.PlayAnim("InBlack", viewData.InBlackAnimSeconds);
            isRetry = false;
            await UniTask.Delay(TimeSpan.FromSeconds(viewData.LoadSceneWithBlackDelaySeconds), cancellationToken: token);
        }

        public void OpenStage()
        {
            if (isRetry)
            {
                frontView.Initialize(frontOpenRellInitSprite);
                frontView.PlayAnim("OpenRell", viewData.OpenAnimSeconds);
            }
            else
            {
                frontView.Initialize(frontOpenInitSprite);
                frontView.PlayAnim("Open", viewData.OpenAnimSeconds);
            }
        }

        public void SetTimeLimit(int timeLimit)
        {
            if (timeLimitText == null)
                return;
            timeLimitText.text = timeLimit.ToString();
        }
    }
}
