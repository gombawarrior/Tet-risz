using Godot;

public abstract class Shape {
    protected abstract int[,] ShapeMatrix { get; }
    protected abstract Vector2 StartPos { get; }
    public abstract int Id { get; }
    public Vector2 pos;
    public int[,] currentShapeMatrix;
    public int CurrentRows => currentShapeMatrix.GetLength(0);
    public int CurrentCols => currentShapeMatrix.GetLength(1);
    public Shape() {
        currentShapeMatrix = ShapeMatrix;
        pos = StartPos;
    }
    
    public void Rotate() {
        int[,] newShapeMatrix = new int[CurrentCols, CurrentRows];
        for (int row = 0; row < CurrentCols; row++) {
            for (int col = 0; col < CurrentRows; col++) {
                newShapeMatrix[row, col] = currentShapeMatrix[CurrentRows - col - 1, row];
            }
        }
        currentShapeMatrix = newShapeMatrix;
    }

    public void RotateBack() {
        int[,] newShapeMatrix = new int[CurrentCols, CurrentRows];
        for (int row = 0; row < CurrentCols; row++) {
            for (int col = 0; col < CurrentRows; col++) {
                newShapeMatrix[row, col] = currentShapeMatrix[col, CurrentCols - row - 1];
            }
        }
        currentShapeMatrix = newShapeMatrix;
    }


    public void Move(int rows, int cols) {
        pos.X += rows;
        pos.Y += cols;
    }

    public void Reset() {
        pos = StartPos;
        currentShapeMatrix = ShapeMatrix;
    }
}