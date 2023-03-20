using Godot;

public partial class Manager
{
	private Shape _activeShape;
	public Shape ActiveShape {
		get => _activeShape;
		set {
			_activeShape = value;
			_activeShape.Reset();
		}
	}
	public Grid Grid;
	public Queue Queue;
	public bool IsGameOver => !(Grid.IsEmptyRow(0) && Grid.IsEmptyRow(1));
	public int Score { get; private set; }

	public Manager() {
		Grid = new Grid(22, 10);
		Queue = new Queue();
		ActiveShape = Queue.UpdateShape();
	}

	private bool IsLegal() {
		for (int row = 0; row < ActiveShape.CurrentRows; row++) {
			for (int col = 0; col < ActiveShape.CurrentCols; col++)
			{
				if (ActiveShape.CurrentShapeMatrix[row, col] != 1) continue;

				if (!Grid.IsEmpty((int)ActiveShape.Pos.X + row, (int)ActiveShape.Pos.Y + col)) return false;
			}
		}
		return true;
	}

	public void RotateShape() {
		ActiveShape.Rotate();
		if (!IsLegal()) {
			ActiveShape.RotateBack();
		}
	}

	public void MoveRight() {
		ActiveShape.Move(0, 1);
		if (!IsLegal()) {
			ActiveShape.Move(0, -1);
		}
	}    

	public void MoveLeft() {
		ActiveShape.Move(0, -1);
		if (!IsLegal()) {
			ActiveShape.Move(0, 1);
		}
	}   

	public void MoveDown() {
		ActiveShape.Move(1, 0);
		if (!IsLegal()) {
			ActiveShape.Move(-1, 0);
			PlaceShapes();
		}
	}

	private void PlaceShapes() {
		for (int row = 0; row < ActiveShape.CurrentRows; row++) {
			for (int col = 0; col < ActiveShape.CurrentCols; col++) {
				if (ActiveShape.CurrentShapeMatrix[row, col] != 1) continue;

				Grid[(int)ActiveShape.Pos.X + row, (int)ActiveShape.Pos.Y + col] = ActiveShape.Id;
			}
		}

		Score += Grid.ClearFullRows();

		if (!IsGameOver) {
			ActiveShape = Queue.UpdateShape();
		}
	}
}
