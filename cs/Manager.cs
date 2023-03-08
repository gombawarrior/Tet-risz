using Godot;

public partial class Manager {
    public Shape ActiveShape {
        get => ActiveShape;
        set {
            ActiveShape = value;
            ActiveShape.Reset();
        }
    }
    public Grid grid;
    public Queue queue;
    public bool IsGameOver => !(grid.IsEmptyRow(0) && grid.IsEmptyRow(1));
    public int Score { get; private set; }

    public Manager() {
        grid = new Grid(22, 20);
        queue = new Queue();
        ActiveShape = queue.UpdateShape();
    }

    private bool IsLegal() {
        for (int row = 0; row < ActiveShape.CurrentCols; row++) {
            for (int col = 0; col < ActiveShape.CurrentRows; col++)
            {
                if (ActiveShape.currentShapeMatrix[row, col] != 1) continue;

                if (!grid.IsEmpty((int)ActiveShape.pos.X + col, (int)ActiveShape.pos.Y + row)) return false;
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
        for (int row = 0; row < ActiveShape.CurrentCols; row++) {
            for (int col = 0; col < ActiveShape.CurrentRows; col++) {
                if (ActiveShape.currentShapeMatrix[row, col] != 1) continue;

                grid[(int)ActiveShape.pos.X + row, (int)ActiveShape.pos.Y + col] = ActiveShape.Id;
            }
        }

        Score += grid.ClearFullRows();

        if (!IsGameOver) {
            ActiveShape = queue.UpdateShape();
        }
    }
}