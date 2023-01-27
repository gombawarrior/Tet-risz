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

    private bool IsLegal(int[,] shapeMatrix) {
        for (int row = 0; row < shapeMatrix.GetLength(1); row++) {
            for (int col = 0; col < shapeMatrix.GetLength(0); col++) {
                if (shapeMatrix[row, col] == 1) {
                    if (!grid.IsEmpty((int)ActiveShape.pos.x + col, (int)ActiveShape.pos.y + row)) return false;
                }
            }
        }
        return true;
    }

    public void RotateShape() {
        var newShape = ActiveShape.Rotate();
        if (IsLegal(newShape)) {
            ActiveShape.currentShapeMatrix = newShape;
        }
    }
}