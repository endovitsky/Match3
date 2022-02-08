using Helpers;
using UniRx;
using UnityEngine;
using Utils;

namespace Models
{
    public class PieceModel: ISelectable
    {
        public Color Color => _color;
        public ReactiveProperty<SquareModel> SquareModel = new ReactiveProperty<SquareModel>();

        private Color _color;

        public PieceModel(Color color)
        {
            _color = color;

            SquareModel.Pairwise().Subscribe(OnSquareModelChanged);
        }

        private void OnSquareModelChanged(Pair<SquareModel> pair)
        {
            Debug.Log($"{this.GetType().Name}.{ReflectionHelper.GetCallerMemberName()}" +
                      $"\n{pair.Previous?.Position}->{pair.Current?.Position}");

            if (pair.Previous?.PieceModel != null)
            {
                pair.Previous.PieceModel.Value = null;
            }
            if (pair.Current?.PieceModel != null)
            {
                pair.Current.PieceModel.Value = this;
            }
        }
    }
}
