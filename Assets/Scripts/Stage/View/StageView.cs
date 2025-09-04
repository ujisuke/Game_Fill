using System;
using System.Collections.Generic;
using System.Threading;
using Assets.Scripts.Common.Data;
using Assets.Scripts.Common.View;
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
        [SerializeField] private ViewData stageViewData;
        [SerializeField] private List<GameObject> additionalObjectList;
        private static bool isRetry = false;
        private BlockView[,] blockMap;

        private void Awake()
        {
            slowEffectView.SetColor(stageViewData.SlowEffectColor);
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
            slowEffectView.PlayAnim("SlowIn", stageViewData.SlowInAnimSeconds);
        }

        public void StopSlowEffect()
        {
            slowEffectView.PlayAnim("SlowOut");
        }

        public async UniTask PlayClearEffect(CancellationToken token)
        {
            await PlayGoodAndText(token);
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.CloseAnimDelaySeconds), cancellationToken: token);
            await CloseStage(token);
        }

        public async UniTask PlayClearFinalEffect(CancellationToken token)
        {
            await PlayGoodAndText(token);
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.ClearFinalAnimDelaySeconds), cancellationToken: token);
            fillEffectView.PlayAnim("ClearFinal", stageViewData.ClearAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.CloseAnimDelaySeconds), cancellationToken: token);
            frontView.PlayAnim("InBlack", stageViewData.InBlackAnimSeconds);
            isRetry = false;
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.LoadSceneWithBlackDelaySeconds), cancellationToken: token);
        }

        private async UniTask PlayGoodAndText(CancellationToken token)
        {
            StopAllBlocks();
            screenView.PlayAnim("Stop");
            GoodView.transform.position = PlayerModel.Instance.Pos + Vector2.up;
            GoodView.PlayAnim("Good", stageViewData.GoodAnimSeconds);
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.ClearAnimDelaySeconds), cancellationToken: token);
            screenView.PlayAnim("Play");
            GoodView.PlayAnim("Empty");
            timeLimitText.enabled = false;
            for (int i = 0; i < additionalObjectList.Count; i++)
                Destroy(additionalObjectList[i]);
            fillEffectView.PlayAnim("Clear", stageViewData.ClearAnimSeconds);
            await PaintStage(token);
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.ClearTextDelaySeconds), cancellationToken: token);
            clearTextView.PlayAnim("Awake", stageViewData.ClearTextAnimSeconds);
        }

        private async UniTask PaintStage(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.PaintStageDelaySeconds), cancellationToken: token);
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
            frontView.PlayAnim("Close", stageViewData.CloseAnimSeconds);
            isRetry = false;
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.LoadSceneDelaySeconds), cancellationToken: token);
        }

        public async UniTask CloseStageRetry(CancellationToken token)
        {
            frontView.PlayAnim("CloseRell", stageViewData.CloseAnimSeconds);
            isRetry = true;
            await UniTask.Delay(TimeSpan.FromSeconds(stageViewData.LoadSceneDelaySeconds), cancellationToken: token);
        }

        public void OpenStage()
        {
            if (isRetry)
            {
                frontView.Initialize(frontOpenRellInitSprite);
                frontView.PlayAnim("OpenRell", stageViewData.OpenAnimSeconds);
            }
            else
            {
                frontView.Initialize(frontOpenInitSprite);
                frontView.PlayAnim("Open", stageViewData.OpenAnimSeconds);
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
