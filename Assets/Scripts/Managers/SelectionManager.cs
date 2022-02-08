using System;
using System.Collections.Generic;
using Models;
using UnityEngine;
using Utils;

namespace Managers
{
    public class SelectionManager : BaseManager<SelectionManager>
    {
        public Action<ISelectable> SelectableAdded = delegate {  };
        public Action<List<ISelectable>> SelectedObjectsChanged = delegate {  };

        public List<ISelectable> SelectedObjects
        {
            get
            {
                return _selectedObjects;
            }
        }

        private List<ISelectable> _selectedObjects = new List<ISelectable>();

        public override void Initialize()
        {
        }
        public override void UnInitialize()
        {
        }

        public override void Subscribe()
        {
        }
        public override void UnSubscribe()
        {
        }

        public void Select(ISelectable selectable)
        {
            ClearSelectedObjects();

            AddObjectToSelectedObjects(selectable);

            Debug.Log($"{this.GetType().Name}.{nameof(SelectedObjectsChanged)}" +
                      $"\n {SelectedObjects.Count}");

            SelectedObjectsChanged.Invoke(SelectedObjects);
        }
        public void AddObjectToSelectedObjects(ISelectable selectable)
        {
            if (IsSelected(selectable))
            {
                return;
            }

            SelectedObjects.Add(selectable);

            var pieceModel = selectable as PieceModel;

            Debug.Log($"{this.GetType().Name}.{nameof(AddObjectToSelectedObjects)}" +
                      $"\n {nameof(SelectedObjects)}.{nameof(SelectedObjects.Count)} = {SelectedObjects.Count} " +
                      $"{pieceModel.SquareModel.Value.Position}");

            SelectableAdded.Invoke(selectable);
        }

        public void ClearSelectedObjects()
        {
            SelectedObjects.Clear();
        }

        public bool IsSelected(ISelectable selectable)
        {
            var isSelected = SelectedObjects.Contains(selectable);
            return isSelected;
        }
    }
}
