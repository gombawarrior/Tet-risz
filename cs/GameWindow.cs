using Godot;
using System;

public class GameField : Node2D {
    Color[] BlockColors = {
        Color.ColorN("black", 0),
        Color.ColorN("cyan"),
        Color.ColorN("yellow"),
        Color.ColorN("green"),
        Color.ColorN("red"),
        Color.ColorN("blue"),
        Color.ColorN("orange"),
        Color.ColorN("purple")
    };

    Manager gameManager = new Manager();
    ColorRect[,] blockControls;
    public override void _Ready() {
        blockControls = ConstructGameField(gameManager.grid);
    }

    private ColorRect[,] ConstructGameField(Grid grid) {
        blockControls = new ColorRect[grid.Rows, grid.Columns];
        int cellSize = 20;

        for (int rows = 0; rows < grid.Rows; rows++) {
            for (int cols = 0;  cols < grid.Columns; cols++) {
                ColorRect blockControl = new ColorRect();
                blockControl.RectSize = new Vector2(cellSize, cellSize);
                Node2D gameCanvas = GetNode<Node2D>("MainField/GameCanvas");
                gameCanvas.AddChild(blockControl);
                blockControls[rows, cols] = blockControl;
            }
        }

        return blockControls;
    }
}
