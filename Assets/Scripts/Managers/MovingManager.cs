using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class MovingManager : BaseManager<MovingManager>
    {
        public Action PieceModelsMoved = delegate {  };

        [SerializeField]
        private Direction direction = Direction.Left;

        public override void Initialize()
        {
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
        }
        protected override async void SubscribeAsync()
        {
            await TaskHelper.WaitUntil(() => PiecesManager.Instance != null);

            PiecesManager.Instance.PieceModelsRemoved += PiecesManagerOnPieceModelsRemoved;
        }
        public override void UnSubscribe()
        {
            if (PiecesManager.Instance != null)
            {
                PiecesManager.Instance.PieceModelsRemoved -= PiecesManagerOnPieceModelsRemoved;
            }
        }

        private void PiecesManagerOnPieceModelsRemoved(List<PieceModel> pieceModels)
        {
            TryMovePieceModelsRecursively(Direction.Bot);
            TryMovePieceModelsRecursively(direction);

            PieceModelsMoved.Invoke();
        }

        private void TryMovePieceModelsRecursively(Direction direction)
        {
            var botPieceModels = PiecesManager.Instance.PieceModels.Where(x =>
                x.SquareModel.Value.GetConnectedSquareModel(direction) != null &&
                x.SquareModel.Value.GetConnectedSquareModel(direction).PieceModel.Value == null).ToList();
            Debug.Log($"{nameof(botPieceModels)}.{nameof(botPieceModels.Count)} = {botPieceModels.Count}");
            if (botPieceModels.Count == 0)
            {
                return;
            }

            foreach (var botPieceModel in botPieceModels)
            {
                botPieceModel.SquareModel.Value = botPieceModel.SquareModel.Value.GetConnectedSquareModel(direction);
            }

            TryMovePieceModelsRecursively(direction);
        }
    }
}
