using System.Collections.Generic;
using Utils;

namespace Models
{
	public class BoardModel
	{
		public List<List<SquareModel>> SquareModels => _squareModels;

		private readonly List<List<SquareModel>> _squareModels;

		public BoardModel(int rowsCount, int cellsCount)
		{
			_squareModels = new List<List<SquareModel>>();
			PlaceSquaresOnBoard(_squareModels, rowsCount, cellsCount);
			LinkSquaresOnBoard(_squareModels);
		}

		private void PlaceSquaresOnBoard(List<List<SquareModel>> squareModels, int rowsCount, int cellsCount)
		{
			for (var rowsIndex = 0; rowsIndex < rowsCount; rowsIndex++)
			{
				var squaresRow = new List<SquareModel>();
				squareModels.Add(squaresRow);
				for (var cellIndex = 0; cellIndex < cellsCount; cellIndex++)
				{
					var position = new Position(rowsIndex, cellIndex);
					squareModels[rowsIndex].Add(new SquareModel(position));
				}
			}
		}
		private void LinkSquaresOnBoard(List<List<SquareModel>> squareModels)
		{
			for (var rowsIndex = 0; rowsIndex < squareModels.Count; rowsIndex++)
			{
				for (var cellIndex = 0; cellIndex < squareModels[rowsIndex].Count; cellIndex++)
				{
					if (cellIndex < squareModels[rowsIndex].Count - 1)
					{
						squareModels[rowsIndex][cellIndex].SetConnectedSquareModel(Direction.Top, squareModels[rowsIndex][cellIndex + 1]);
					}
					if (cellIndex > 0)
					{
						squareModels[rowsIndex][cellIndex].SetConnectedSquareModel(Direction.Bot, squareModels[rowsIndex][cellIndex - 1]);
					}

					if (rowsIndex > 0)
					{
						squareModels[rowsIndex][cellIndex].SetConnectedSquareModel(Direction.Left, squareModels[rowsIndex - 1][cellIndex]);
					}
					if (rowsIndex < squareModels.Count - 1)
					{
						squareModels[rowsIndex][cellIndex].SetConnectedSquareModel(Direction.Right, squareModels[rowsIndex + 1][cellIndex]);
					}
				}
			}
		}
	}
}
