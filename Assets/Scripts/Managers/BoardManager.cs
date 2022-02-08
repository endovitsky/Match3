using System;
using System.Linq;
using Helpers;
using Models;
using UniRx;
using UnityEngine;

namespace Managers
{
    public class BoardManager : BaseManager<BoardManager>
    {
        [SerializeField]
        private int _rowsCount;
        [SerializeField]
        private int _cellsCount;

        public ReactiveProperty<BoardModel> BoardModel;

        private IDisposable _boardModelOnChangedSubscription;

        public override void Initialize()
        {
            BoardModel = new ReactiveProperty<BoardModel>()
            {
                Value = new BoardModel(_rowsCount, _cellsCount)
            };
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
            _boardModelOnChangedSubscription = BoardModel.Subscribe(BoardModelOnChanged);
        }
        public override void UnSubscribe()
        {
            _boardModelOnChangedSubscription?.Dispose();
        }

        public SquareModel GetFreeSquareModel()
        {
            var squareModelsRowWithFreeSquare =
                BoardModel.Value.SquareModels.FirstOrDefault(x => x.Any(y => y.PieceModel.Value == null));
            var freeSquareModel = squareModelsRowWithFreeSquare?.FirstOrDefault(x => x.PieceModel.Value == null);

            return freeSquareModel;
        }
        private void BoardModelOnChanged(BoardModel boardModel)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}");
        }
    }
}
