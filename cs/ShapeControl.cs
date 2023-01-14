using Godot;
using System.Linq;

public abstract class ShapeControl {
    protected abstract int[,] ShapeMatrix { get; }
    protected abstract Vector2 StartPos { get; }
    public abstract int Id { get; }
    public Vector2 pos;
    public int[,] currentShapeMatrix;
    public ShapeControl() {
        currentShapeMatrix = ShapeMatrix;
        pos = StartPos;
    }
    
    public int[,] Rotate() {
        int[,] newShapeMatrix = new int[currentShapeMatrix.GetLength(1), currentShapeMatrix.GetLength(0)];
        for (int row = 0; row < newShapeMatrix.GetLength(1); row++) {
            for (int col = 0; col < currentShapeMatrix.GetLength(0); col++) {
                currentShapeMatrix[row, col] = currentShapeMatrix[col, currentShapeMatrix.GetLength(1) - row - 1];
            }
        }
        return newShapeMatrix;
    }
    public void Move(int rows, int cols) {
        pos.x += rows;
        pos.y += cols;
    }
    public void Reset() {
        pos = StartPos;
        currentShapeMatrix = ShapeMatrix;
    }
}