using System;
using System.Threading;
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
        private static bool isRetry = false;
        private BlockView[,] blockMap;
        private CancellationTokenSource cTS;
        private CancellationToken token;

        private void Awake()
        {
            cTS = new();
            token = cTS.Token;
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
            slowEffectView.PlayAnim("SlowIn", 0.3f);
        }

        public void StopSlowEffect()
        {
            slowEffectView.PlayAnim("SlowOut");
        }

        public async UniTask PlayClearEffect()
        {
            StopAllBlocks();
            screenView.PlayAnim("Stop");
            GoodView.transform.position = PlayerModel.Instance.Pos + Vector2.up;
            GoodView.PlayAnim("Good");
            await UniTask.Delay(TimeSpan.FromSeconds(1f), cancellationToken: token);
            screenView.PlayAnim("Play");
            GoodView.PlayAnim("Empty");
            timeLimitText.enabled = false;
            fillEffectView.PlayAnim("Clear", 0.3f);
            await PaintStage();
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: token);
            clearTextView.PlayAnim("Awake", 0.25f);
            await UniTask.Delay(TimeSpan.FromSeconds(1.5f));
            CloseStage(false);
            await UniTask.Delay(TimeSpan.FromSeconds(1f));
        }

        private async UniTask PaintStage()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.3f), cancellationToken: token);
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

        public void CloseStage(bool isRetryCurrent)
        {
            if (isRetryCurrent)
                frontView.PlayAnim("CloseRell", 0.25f);
            else
                frontView.PlayAnim("Close", 0.25f);
            isRetry = isRetryCurrent;
        }

        public void OpenStage()
        {
            if (isRetry)
            {
                frontView.SetSprite(frontOpenRellInitSprite);
                frontView.PlayAnim("OpenRell", 0.25f);
            }
            else
                frontView.PlayAnim("Open", 0.25f);
        }

        public void SetTimeLimit(int timeLimit)
        {
            if (timeLimitText == null)
                return;
            timeLimitText.text = timeLimit.ToString();
        }

        public void OnDestroy()
        {
            cTS?.Cancel();
        }
    }
}
