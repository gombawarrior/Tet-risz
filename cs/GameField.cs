using Godot;
using System;
using System.Threading.Tasks;

public partial class GameField : ColorRect {
	Color[] shapeColors = {
		new("black", 0.5f),
		new("cyan"),
		new("yellow"),
		new("green"),
		new("red"),
		new("blue"),
		new("orange"),
		new("purple")
	};

	float delay = 1000;
    double nextMove = 0.075;
    bool firstPressed = true;
    Manager gameManager = new();
	Node2D gameCanvas;
    Node2D nextView;
	ColorRect[,] shapeControls;
	ColorRect[,] nextShapeControls;
    public override async void _Ready() {
		gameCanvas = GetNode<Node2D>("MainField/GameCanvas");
        nextView = GetNode<Node2D>("Scoreboard/ColorRect/NextView");
		shapeControls = ConstructGameField(gameManager.grid);
        nextShapeControls = ConstructNextShapeField();
        await GameLoop();
	}

    //public override void _Input(InputEvent @event) {
    public override void _Process(double delta) {
		nextMove -= delta;

        if (nextMove > 0) return;
        bool rotatePressed = Input.IsActionJustPressed("rotate");
        bool leftPressed = Input.IsActionPressed("left");
        bool rightPressed = Input.IsActionPressed("right");
        bool downPressed = Input.IsActionPressed("down");

        if (rotatePressed) gameManager.RotateShape();
        if (leftPressed) gameManager.MoveLeft();
        if (rightPressed) gameManager.MoveRight();
        if (downPressed) gameManager.MoveDown();

   //     if (firstPressed) {
   //         Task.Delay(100);
			//firstPressed = false;
   //     }

		if (rotatePressed || leftPressed || rightPressed || downPressed) {
            nextMove = 0.075;
        }

        Draw(gameManager);
    }

    private ColorRect[,] ConstructGameField(Grid grid) {
		shapeControls = new ColorRect[grid.Rows, grid.Columns];
		int cellSize = 40;
		int padding = 0;

		for (int rows = 0; rows < grid.Rows; rows++) {
			for (int cols = 0;  cols < grid.Columns; cols++) {
				ColorRect shapeControl = new();
				shapeControl.Color = shapeColors[0];
				shapeControl.Size = new Vector2(cellSize, cellSize);
				shapeControl.Position = new Vector2(cols * cellSize + padding, (rows - 2) * cellSize + padding);
				gameCanvas.AddChild(shapeControl);
				shapeControls[rows, cols] = shapeControl;
			}
		}

		return shapeControls;
	}

    private ColorRect[,] ConstructNextShapeField() {
        nextShapeControls = new ColorRect[4, 4];
        int cellSize = 20;
        int padding = 0;

        for (int rows = 0; rows < 4; rows++) {
            for (int cols = 0; cols < 4; cols++) {
                ColorRect shapeControl = new();
                shapeControl.Color = shapeColors[0];
                shapeControl.Size = new Vector2(cellSize, cellSize);
                shapeControl.Position = new Vector2(cols * cellSize + padding, (rows - 2) * cellSize + padding);
                nextView.AddChild(shapeControl);
                nextShapeControls[rows, cols] = shapeControl;
            }
        }

        return nextShapeControls;
    }

    private void DrawGrid(Grid grid) {
		for (int rows = 0; rows < grid.Rows; rows++) {
			for (int cols = 0; cols < grid.Columns; cols++) {
				int id = grid[rows, cols];
				shapeControls[rows, cols].Color = shapeColors[id];
			}
		}
	}

	private void DrawShapes(Shape shape) {
		for (int row = 0; row < shape.CurrentRows; row++) {
			for (int col = 0; col < shape.CurrentCols; col++) {
				if (shape.currentShapeMatrix[row, col] == 1) {
					shapeControls[(int)shape.pos.X + row, (int)shape.pos.Y + col].Color = shapeColors[shape.Id];
				}
			}
		}
	}

    private void DrawNext(Shape nextShape) {
		
        for (int row = 0; row < nextShape.CurrentRows; row++) {
            for (int col = 0; col < nextShape.CurrentCols; col++) {
                if (nextShape.currentShapeMatrix[row, col] == 1) {
                    nextShapeControls[row, col].Color = shapeColors[nextShape.Id];
                }
            }
        }
    }

	private void Draw(Manager gameManager) {
		DrawGrid(gameManager.grid);
		DrawShapes(gameManager.ActiveShape);
		DrawNext(gameManager.queue.NextShape);
		Label lineText = GetNode<Label>("Lines/LineText");
		lineText.Text = $"LINES: {gameManager.Score:0000}";
	}

	private async Task GameLoop() {
		Draw(gameManager);

		while (!gameManager.IsGameOver) {
			await Task.Delay(TimeSpan.FromMilliseconds(delay));
			gameManager.MoveDown();
			Draw(gameManager);
		}
	}
}
