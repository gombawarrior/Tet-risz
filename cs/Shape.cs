using Godot;

public abstract class Shape {
    protected abstract int[,] ShapeMatrix { get; }
    protected abstract Vector2 StartPos { get; }
    public abstract int Id { get; }
    public Vector2 Pos;
    public int[,] CurrentShapeMatrix;
    public int CurrentRows => CurrentShapeMatrix.GetLength(0);
    public int CurrentCols => CurrentShapeMatrix.GetLength(1);
    protected Shape() {
        CurrentShapeMatrix = ShapeMatrix;
        Pos = StartPos;
    }
    
    public void Rotate() {
        int[,] newShapeMatrix = new int[CurrentCols, CurrentRows];
        for (int row = 0; row < CurrentCols; row++) {
            for (int col = 0; col < CurrentRows; col++) {
                newShapeMatrix[row, col] = CurrentShapeMatrix[CurrentRows - col - 1, row];
            }
        }
        CurrentShapeMatrix = newShapeMatrix;
    }

    public void RotateBack() {
        int[,] newShapeMatrix = new int[CurrentCols, CurrentRows];
        for (int row = 0; row < CurrentCols; row++) {
            for (int col = 0; col < CurrentRows; col++) {
                newShapeMatrix[row, col] = CurrentShapeMatrix[col, CurrentCols - row - 1];
            }
        }
        CurrentShapeMatrix = newShapeMatrix;
    }


    public void Move(int rows, int cols) {
        Pos.X += rows;
        Pos.Y += cols;
    }

    public void Reset() {
        Pos = StartPos;
        CurrentShapeMatrix = ShapeMatrix;
    }
}