using Godot;
using System.Linq;
using System.Collections.Generic;

public abstract class ShapeControl {
    protected abstract List<List<int>> ShapeMatrix { get; }
    protected abstract Vector2 StartPos { get; }
    public abstract int Id { get; }
    public Vector2 pos;
    public List<List<int>> currentShapeMatrix;
    public ShapeControl() {
        currentShapeMatrix = ShapeMatrix;
        pos = StartPos;
    }
    
    public List<List<int>> Rotate() {
        List<List<int>> newShapeMatrix = new List<List<int>>();
        for (int row = 0; row < currentShapeMatrix[0].Count; row++) {
            for (int col = 0; col < currentShapeMatrix.Count; col++) {
                newShapeMatrix[row][col] = currentShapeMatrix[col][currentShapeMatrix[0].Count - row - 1];
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