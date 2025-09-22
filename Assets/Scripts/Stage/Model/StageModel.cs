using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Stage.Model
{
    public class StageModel
    {
        private readonly BlockModel[,] blockMap;
        private readonly StageController stageController;
        private int timeLimit;
        private static StageModel instance;

        public int TimeLimit => timeLimit;
        public static StageModel Instance => instance;

        public StageModel(Tilemap tilemap, StageController stageController, int timeLimit)
        {
            blockMap = GetBlockMap(tilemap);
            instance = this;
            this.stageController = stageController;
            this.timeLimit = timeLimit;
        }

        private static BlockModel[,] GetBlockMap(Tilemap tilemap)
        {
            Vector2 maxPos = Vector2.zero;
            for (int i = 0; i < tilemap.gameObject.transform.childCount; i++)
            {
                Vector2 pos = tilemap.gameObject.transform.GetChild(i).position;
                maxPos = Vector2.Max(maxPos, pos);
            }
            BlockModel[,] newBlockMap = new BlockModel[(int)maxPos.x + 1, (int)maxPos.y + 1];
            for (int i = 0; i < tilemap.gameObject.transform.childCount; i++)
            {
                BlockController blockController = tilemap.gameObject.transform.GetChild(i).GetComponent<BlockController>();
                blockController.Initialize();
                Vector2Int blockIndex = ConvertPosToIndex(blockController.transform.position);
                newBlockMap[blockIndex.x, blockIndex.y] = blockController.BlockModel;
            }
            return newBlockMap;
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

        public bool IsPlayerHittingWall(HurtBox playerHurtBox)
        {
            Vector2Int leftTopIndex = ConvertPosToIndex(playerHurtBox.LeftTopPos);
            Vector2Int rightTopIndex = ConvertPosToIndex(new Vector2(playerHurtBox.RightBottomPos.x, playerHurtBox.LeftTopPos.y));
            Vector2Int rightBottomIndex = ConvertPosToIndex(playerHurtBox.RightBottomPos);
            Vector2Int leftBottomIndex = ConvertPosToIndex(new Vector2(playerHurtBox.LeftTopPos.x, playerHurtBox.RightBottomPos.y));
            return IsWallPos(leftTopIndex) || IsWallPos(rightTopIndex)
                || IsWallPos(rightBottomIndex) || IsWallPos(leftBottomIndex);
        }

        public bool IsPlayerOnBlock(Vector2 playerPos)
        {
            Vector2Int playerIndex = ConvertPosToIndex(playerPos);
            return DoesExistBlock(playerIndex);
        }

        public bool IsPlayerOnExit(Vector2 playerPos)
        {
            Vector2Int playerIndex = ConvertPosToIndex(playerPos);
            if (!DoesExistBlock(playerIndex))
                return false;
            return blockMap[playerIndex.x, playerIndex.y].IsExit;
        }

        private bool DoesExistBlock(Vector2Int posIndex)
        {
            if (posIndex.x < 0 || posIndex.x >= blockMap.GetLength(0) || posIndex.y < 0 || posIndex.y >= blockMap.GetLength(1))
                return false;
            if (blockMap[posIndex.x, posIndex.y] == null)
                return false;
            return true;
        }

        private bool IsWallPos(Vector2Int playerPosIndex)
        {
            if (!DoesExistBlock(playerPosIndex))
                return false;
            return blockMap[playerPosIndex.x, playerPosIndex.y].IsWall;
        }

        public void FillBlock(HurtBox playerHurtBox)
        {
            Vector2Int playerPosIndex = ConvertPosToIndex(playerHurtBox.Pos);
            if (!DoesExistBlock(playerPosIndex))
                return;
            BlockModel block = blockMap[playerPosIndex.x, playerPosIndex.y];
            if (block.CanBeFilled)
                block.Fill().Forget();
        }

        public bool IsAllBlockFilled()
        {
            foreach (BlockModel block in blockMap)
            {
                if (block == null)
                    continue;
                if (block.CanBeFilled)
                    return false;
            }
            return true;
        }

        public void DestroyAllBlock()
        {
            foreach (BlockModel block in blockMap)
                block?.OnDestroy();
        }

        public void PlaySlowEffect()
        {
            stageController.PlaySlowEffect();
        }

        public void StopSlowEffect()
        {
            stageController.StopSlowEffect();
        }

        public async UniTask CountDownTimer(CancellationToken token)
        {
            while (timeLimit > 0)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
                if (PlayerModel.Instance == null || PlayerModel.Instance.IsOnExit)
                    break;
                timeLimit--;
                stageController.SetTimeLimit(timeLimit);
            }
        }
    }
}
