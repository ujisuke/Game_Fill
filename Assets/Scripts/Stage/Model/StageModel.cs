using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Stage.Model
{
    public class StageModel
    {
        private readonly BlockModel[,] blockMap;
        private readonly List<BlockModel> visitedBlockList;
        private static StageModel instance;
        public static StageModel Instance => instance;

        public StageModel(Tilemap tilemap)
        {
            blockMap = GetBlockMap(tilemap);
            visitedBlockList = new();
            instance = this;
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
            for (int i = visitedBlockList.Count - 1; i >= 0; i--)
            {
                BlockModel visitedBlock = visitedBlockList[i];
                if (math.abs(visitedBlock.Pos.x - playerHurtBox.Pos.x) >= 0.6f + playerHurtBox.Scale.x * 0.5f || math.abs(visitedBlock.Pos.y - playerHurtBox.Pos.y) >= 0.6f + playerHurtBox.Scale.y * 0.5f)
                {
                    visitedBlock.SetWall();
                    visitedBlockList.RemoveAt(i);
                }
            }
            
            Vector2Int playerPosIndex = ConvertPosToIndex(playerHurtBox.Pos);
            if (!DoesExistBlock(playerPosIndex))
                return;
            BlockModel block = blockMap[playerPosIndex.x, playerPosIndex.y];
            if (!block.IsVisited)
            {
                block.Visit();
                visitedBlockList.Add(block);
            }
        }
    }
}
