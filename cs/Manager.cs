using Godot;

public class Manager {
    private ShapeControl ActiveShape {
        get => ActiveShape;
        set {
            ActiveShape = value;
            ActiveShape.Reset();
        }
    }
    Grid grid;

    public Manager() {
        grid = new Grid(22, 20);
    }

    private bool IsLegal() {
        for (int row = 0; row < ActiveShape.CurrentCols; row++) {
            for (int col = 0; col < ActiveShape.CurrentRows; col++) {
                if (ActiveShape.currentShapeMatrix[row, col] == 1) {
                    if (!grid.IsEmpty((int)ActiveShape.pos.x + col, (int)ActiveShape.pos.y + row)) return false;
                }
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
            PlaceBlocks();
        }
    }

    private void PlaceBlocks() {
        for (int row = 0; row < ActiveShape.CurrentCols; row++) {
            for (int col = 0; col < ActiveShape.CurrentRows; col++) {
                if (ActiveShape.currentShapeMatrix[row, col] == 1) {
                    grid[(int)ActiveShape.pos.x + row, (int)ActiveShape.pos.y + col] = ActiveShape.Id;
                }
            }
        }
    }
}