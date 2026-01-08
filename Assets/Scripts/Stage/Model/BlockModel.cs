using UnityEngine;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Assets.Scripts.Common.Data;

namespace Assets.Scripts.Stage.Model
{
    public class BlockModel
    {
        private readonly BlockController blockController;
        private readonly ViewData viewData;
        private Vector2 pos;
        private readonly HitBox hitBox;
        private bool isWall;
        private bool canBeFilled;
        private readonly bool isExit;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;
        private bool isWallInitial;
        public Vector2 Pos => pos;
        public HitBox HitBox => hitBox;
        public bool IsWall => isWall;
        public bool CanBeFilled => canBeFilled;
        public bool IsExit => isExit;
        public bool IsWallInitial => isWallInitial;

        public BlockModel(BlockController blockController, Vector2 pos, Vector2 scale, bool isWall, bool canBeFilled, bool isExit, ViewData viewData)
        {
            this.viewData = viewData;
            this.blockController = blockController;
            this.pos = pos;
            hitBox = new HitBox(pos, scale);
            this.isWall = isWall;
            this.canBeFilled = canBeFilled;
            this.isExit = isExit;
            cTS = new();
            token = cTS.Token;
            isWallInitial = isWall;
        }

        public async UniTask Fill()
        {
            canBeFilled = false;
            blockController.PlayAnim("Filling", viewData.BlockBecomeWallSeconds);
            float wallDeltaSeconds = viewData.BlockBecomeWallSeconds * 0.01f;
            float filledAnimSeconds = viewData.BlockFilledAnimSeconds;
            for (int i = 0; i < 100; i++)
            {
                if (PlayerModel.Instance == null)
                    return;
                blockController.SetAnimSpeed(1 / filledAnimSeconds * PlayerModel.Instance.CurrentDecelerationFactor);
                await UniTask.Delay(TimeSpan.FromSeconds(wallDeltaSeconds / PlayerModel.Instance.CurrentDecelerationFactor), cancellationToken: token);
            }
            isWall = true;
            blockController.Fill();
            float remainingDeltaSeconds = (filledAnimSeconds - viewData.BlockBecomeWallSeconds) * 0.01f;
            for (int i = 0; i < 100; i++)
            {
                if (PlayerModel.Instance == null)
                    return;
                blockController.SetAnimSpeed(1 / filledAnimSeconds * PlayerModel.Instance.CurrentDecelerationFactor);
                await UniTask.Delay(TimeSpan.FromSeconds(remainingDeltaSeconds / PlayerModel.Instance.CurrentDecelerationFactor), cancellationToken: token);
            }
        }

        public void OnDestroy()
        {
            cTS.Cancel();
            cTS.Dispose();
        }
    }
}
