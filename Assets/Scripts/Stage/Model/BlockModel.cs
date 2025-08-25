using UnityEngine;
using Assets.Scripts.Player.Model;
using Assets.Scripts.Stage.Controller;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;

namespace Assets.Scripts.Stage.Model
{
    public class BlockModel
    {
        private readonly BlockController blockController;
        private Vector2 pos;
        private readonly HitBox hitBox;
        private bool isWall;
        private bool isVisited;
        private readonly CancellationTokenSource cTS;
        private readonly CancellationToken token;
        public Vector2 Pos => pos;
        public HitBox HitBox => hitBox;
        public bool IsWall => isWall;
        public bool IsVisited => isVisited;

        public BlockModel(BlockController blockController, Vector2 pos, Vector2 scale, bool isWall)
        {
            this.blockController = blockController;
            this.pos = pos;
            hitBox = new HitBox(pos, scale);
            this.isWall = isWall;
            isVisited = false;
            cTS = new();
            token = cTS.Token;
        }

        public async UniTask Fill()
        {
            isVisited = true;
            blockController.PlayAnim("Filled", 0.5f);
            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);
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
