using System.Collections.Generic;
using System.Linq;
using Helpers;
using Managers;
using UniRx;
using UnityEngine;
using Utils;

namespace Models
{
    public class SquareModel
    {
        public Position Position => _position;
        public ReactiveProperty<PieceModel> PieceModel = new ReactiveProperty<PieceModel>();

        public List<SquareModel> ConnectedSquareModels
        {
            get
            {
                return _directionSquareModels.Values.ToList();
            }
        }
        public List<SquareModel> ConnectedSquareModelsOfTheSameColor
        {
            get
            {
                var connectedSquareModels = new List<SquareModel>();
                foreach (var connectedSquareModel in ConnectedSquareModels)
                {
                    if (connectedSquareModel == null)
                    {
                        continue;
                    }

                    if (connectedSquareModel.PieceModel.Value == null)
                    {
                        continue;
                    }

                    if (connectedSquareModel.PieceModel.Value.Color != PieceModel.Value.Color)
                    {
                        continue;
                    }

                    connectedSquareModels.Add(connectedSquareModel);
                }

                return connectedSquareModels;
            }
        }

        private readonly Position _position;
        private Dictionary<Direction, SquareModel> _directionSquareModels = new Dictionary<Direction, SquareModel>();

        public SquareModel(Position position)
        {
            Debug.Log($"{GetType().Name} initialization started");

            _directionSquareModels.Add(Direction.Top,null);
            _directionSquareModels.Add(Direction.Bot,null);
            _directionSquareModels.Add(Direction.Left,null);
            _directionSquareModels.Add(Direction.Right,null);

            _position = position;

            PiecesManager.Instance.PieceModelRemoved += PiecesManagerOnPieceModelRemoved;
            SelectionManager.Instance.SelectableAdded += SelectionManagerOnSelectableAdded;

            PieceModel.Subscribe(PieceModelOnChanged);

            Debug.Log($"{GetType().Name} initialization finished");
        }

        public void SetConnectedSquareModel(Direction direction, SquareModel squareModel)
        {
            _directionSquareModels[direction] = squareModel;
        }
        public SquareModel GetConnectedSquareModel(Direction direction)
        {
            return _directionSquareModels[direction];
        }

        private void SelectionManagerOnSelectableAdded(ISelectable selectable)
        {
            if (PieceModel == null)
            {
                return;
            }
            var pieceModel = selectable as PieceModel;
            if (pieceModel == null)
            {
                return;
            }
            if (pieceModel != PieceModel.Value)
            {
                return;
            }
            if (ConnectedSquareModelsOfTheSameColor.Count < 1)
            {
                return;
            }

            foreach (var connectedSquareModel in ConnectedSquareModelsOfTheSameColor)
            {
                SelectionManager.Instance.AddObjectToSelectedObjects(connectedSquareModel.PieceModel.Value);
            }
        }
        private void PiecesManagerOnPieceModelRemoved(PieceModel pieceModel)
        {
            if (pieceModel != PieceModel.Value)
            {
                return;
            }

            PieceModel = null;
        }
        private void PieceModelOnChanged(PieceModel pieceModel)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}");
        }
    }
}
