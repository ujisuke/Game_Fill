using UnityEngine;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Assets.Scripts.Stage.Data;

namespace Assets.Scripts.Stage.Model
{
    public class BlockModel
    {
        private readonly BlockController blockController;
        private readonly BlockData blockData;
        private Vector2 pos;
        private readonly HitBox hitBox;
        private bool isWall;
        private bool canBeFilled;
        private readonly bool isExit;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;
        public Vector2 Pos => pos;
        public HitBox HitBox => hitBox;
        public bool IsWall => isWall;
        public bool CanBeFilled => canBeFilled;
        public bool IsExit => isExit;

        public BlockModel(BlockController blockController, Vector2 pos, Vector2 scale, bool isWall, bool canBeFilled, bool isExit, BlockData blockData)
        {
            this.blockData = blockData;
            this.blockController = blockController;
            this.pos = pos;
            hitBox = new HitBox(pos, scale);
            this.isWall = isWall;
            this.canBeFilled = canBeFilled;
            this.isExit = isExit;
            cTS = new();
            token = cTS.Token;
        }

        public async UniTask Fill()
        {
            canBeFilled = false;
            blockController.PlayAnim("Filling");
            float wallDeltaSeconds = blockData.BecomeWallSeconds * 0.01f;
            float filledAnimSeconds = blockData.FilledAnimSeconds;
            for (int i = 0; i < 100; i++)
            {
                if (PlayerModel.Instance == null)
                    return;
                blockController.SetAnimSpeed(filledAnimSeconds * PlayerModel.Instance.CurrentDecelerationFactor);
                await UniTask.Delay(TimeSpan.FromSeconds(wallDeltaSeconds / PlayerModel.Instance.CurrentDecelerationFactor), cancellationToken: token);
            }
            isWall = true;
            blockController.Fill();
        }

        public void OnDestroy()
        {
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}
