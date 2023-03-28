using Godot;

namespace Tetrisz;

public partial class Manager {
	private Shape _activeShape;
	public Shape ActiveShape {
		get => _activeShape;
		set {
			_activeShape = value;
			_activeShape.Reset();

            for (int i = 0; i < 2; i++) {
                _activeShape.Move(1, 0);

                if (!IsLegal()) {
                    _activeShape.Move(-1, 0);
                }
            }
        }
	}
	public Grid Grid { get; }
	public Queue Queue { get; }
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
			PlaceShape();
		}
	}

    private int BlockDropDistance(Vector2 pos) {
        int drop = 0;

        while (Grid.IsEmpty((int)pos.X + drop + 1, (int)pos.Y)) {
            drop++;
        }

		return drop;
    }

    public int ShapeDropDistance() {
        int drop = Grid.Rows;

        for (int row = 0; row < ActiveShape.CurrentRows; row++) {
            for (int col = 0; col < ActiveShape.CurrentCols; col++) {
                if (ActiveShape.CurrentShapeMatrix[row, col] == 0) continue;

                Vector2 pos = new Vector2(ActiveShape.Pos.X + row, ActiveShape.Pos.Y + col);
                drop = System.Math.Min(drop, BlockDropDistance(pos));
            }
        }

		return drop;
    }

    public void DropShape() {
		ActiveShape.Move(ShapeDropDistance(), 0);
		PlaceShape();
    }

	private void PlaceShape() {
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
