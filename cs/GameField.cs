using Godot;
using System;
using System.Threading.Tasks;

public partial class GameField : ColorRect {
    private Color[] _shapeColors = {
		new("black", 0.0f),
		new("cyan"),
		new("yellow"),
		new("green"),
		new("red"),
		new("blue"),
		new("orange"),
		new("purple")
	};

    private float _minDelay, _maxDelay, _delayDecrease;
    private double _nextMove = 0.085;
    private Manager _gameManager = new();
    private Node2D _gameCanvas, _nextView;
    private ColorRect[,] _shapeControls, _nextShapeControls;
    private Control _menu;
    public static GameField Instance;

    public override void _Ready() {
        Instance = this;

		_gameCanvas = GetNode<Node2D>("MainField/GameCanvas");
        _nextView = GetNode<Node2D>("Scoreboard/ColorRect/NextView");
        _menu = GetNode<Control>("CanvasLayer/Menu");
		_shapeControls = ConstructGameField(_gameManager.Grid);
        _nextShapeControls = ConstructNextShapeField();
	}

    public override void _Process(double delta) {
            if (_menu.Visible) return;

            _nextMove -= delta;

            if (_nextMove > 0) return;
            bool rotatePressed = Input.IsActionJustPressed("rotate");
            bool leftPressed = Input.IsActionPressed("left");
            bool rightPressed = Input.IsActionPressed("right");
            bool downPressed = Input.IsActionPressed("down");

            if (rotatePressed) _gameManager.RotateShape();
            if (leftPressed) _gameManager.MoveLeft();
            if (rightPressed) _gameManager.MoveRight();
            if (downPressed) _gameManager.MoveDown();

            if (rotatePressed || leftPressed || rightPressed || downPressed) {
                _nextMove = 0.085;
            }

            Draw(_gameManager);
        }

    private ColorRect[,] ConstructGameField(Grid grid) {
		_shapeControls = new ColorRect[grid.Rows, grid.Columns];
		const int cellSize = 40;
		const int padding = 0;

		for (int rows = 0; rows < grid.Rows; rows++) {
			for (int cols = 0;  cols < grid.Columns; cols++) {
				ColorRect shapeControl = new();
				shapeControl.Color = _shapeColors[0];
				shapeControl.Size = new Vector2(cellSize, cellSize);
				shapeControl.Position = new Vector2(cols * cellSize + padding, (rows - 2) * cellSize + padding);
				_gameCanvas.AddChild(shapeControl);
				_shapeControls[rows, cols] = shapeControl;
			}
		}

		return _shapeControls;
	}

    private ColorRect[,] ConstructNextShapeField() {
        _nextShapeControls = new ColorRect[4, 4];
        const int cellSize = 25;
        const int padding = 0;

        for (int rows = 0; rows < 4; rows++) {
            for (int cols = 0; cols < 4; cols++) {
                ColorRect shapeControl = new();
                shapeControl.Color = _shapeColors[0];
                shapeControl.Size = new Vector2(cellSize, cellSize);
                shapeControl.Position = new Vector2(cols * cellSize + padding, rows * cellSize + padding);
                _nextView.AddChild(shapeControl);
                _nextShapeControls[rows, cols] = shapeControl;
            }
        }

        return _nextShapeControls;
    }

    private void DrawGrid(Grid grid) {
		for (int rows = 0; rows < grid.Rows; rows++) {
			for (int cols = 0; cols < grid.Columns; cols++) {
				int id = grid[rows, cols];
				_shapeControls[rows, cols].Color = _shapeColors[id];
			}
		}
	}

	private void DrawShapes(Shape shape) {
		for (int row = 0; row < shape.CurrentRows; row++) {
			for (int col = 0; col < shape.CurrentCols; col++) {
				if (shape.CurrentShapeMatrix[row, col] == 1) {
					_shapeControls[(int)shape.Pos.X + row, (int)shape.Pos.Y + col].Color = _shapeColors[shape.Id];
				}
			}
		}
	}

    private void DrawNext(Shape nextShape) {
		// clean nextView
        for (int row = 0; row < _nextShapeControls.GetLength(0); row++) {
            for (int col = 0; col < _nextShapeControls.GetLength(1); col++) {
                _nextShapeControls[row, col].Color = _shapeColors[0];
            }
        }

        for (int row = 0; row < nextShape.CurrentRows; row++) {
            for (int col = 0; col < nextShape.CurrentCols; col++) {
                if (nextShape.CurrentShapeMatrix[row, col] == 1) {
                    _nextShapeControls[row, col].Color = _shapeColors[nextShape.Id];
                }
            }
        }
    }

	private void Draw(Manager gameManager) {
		DrawGrid(gameManager.Grid);
		DrawShapes(gameManager.ActiveShape);
		DrawNext(gameManager.Queue.NextShape);
		Label lineText = GetNode<Label>("Lines/LineText");
		lineText.Text = $"LINES: {gameManager.Score:0000}";
	}

	private async Task GameLoop() {
		Draw(_gameManager);

		while (!_gameManager.IsGameOver) {
            float delay = Math.Max(_minDelay, _maxDelay - (_gameManager.Score * _delayDecrease));

            await Task.Delay(TimeSpan.FromMilliseconds(delay));
			_gameManager.MoveDown();
			Draw(_gameManager);
        }
        _menu.Visible = true;
    }

    public async void Play_Pressed(string difficulty) {
        _menu.Visible = false;
        switch (difficulty) {
            case "Weak":
                _minDelay = 250;
                _maxDelay = 1000;
                _delayDecrease = 40;
                break;
            case "Med":
                _minDelay = 175;
                _maxDelay = 850;
                _delayDecrease = 50;
                break;
            case "Hard":
                _minDelay = 100;
                _maxDelay = 650;
                _delayDecrease = 70;
                break;
        }

        _gameManager = new Manager();

        await GameLoop();
    }
}
