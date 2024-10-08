using Godot;
using System;
using System.Threading.Tasks;
using Tetrisz;

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

	private bool _isInMainLoop, _gameRunning, _isPaused = false, _isHard = false;
	private float _minDelay, _maxDelay, _delayDecrease;
	private double _nextMove = 0.085;
	private Manager _gameManager = new();
	private Node2D _gameCanvas, _nextView;
	private ColorRect[,] _shapeControls, _nextShapeControls;
	private Label _lineText;
	private Menu _menu;
	private PauseMenu _pauseMenu;
	public static GameField Instance;

	public override void _Ready() {
		Instance = this;

		_lineText = GetNode<Label>("Lines/LineText");
		_gameCanvas = GetNode<Node2D>("MainField/GameCanvas");
		_nextView = GetNode<Node2D>("Scoreboard/ColorRect/NextView");
		_menu = GetNode<Menu>("Menus/MainMenu");
		_pauseMenu = GetNode<PauseMenu>("Menus/PauseMenu");
		_shapeControls = ConstructGameField(_gameManager.Grid);
		_nextShapeControls = ConstructNextShapeField();

		_pauseMenu._Init();
	}

	public override void _Process(double delta) {
        bool pausePressed = Input.IsActionJustPressed("Pause");

        if (pausePressed && _isPaused) {
            PauseContinue_Pressed();
            _isPaused = false;
            return;
        }

        if (!_gameRunning) return;

		_nextMove -= delta;

		if (_nextMove > 0) return;
		bool rotatePressed = Input.IsActionJustPressed("Rotate");
		bool leftPressed = Input.IsActionPressed("Left");
		bool rightPressed = Input.IsActionPressed("Right");
		bool downPressed = Input.IsActionPressed("Down");
		bool dropPressed = Input.IsActionPressed("Drop");

		if (rotatePressed) _gameManager.RotateShape();
		if (leftPressed) _gameManager.MoveLeft();
		if (rightPressed) _gameManager.MoveRight();
		if (downPressed) _gameManager.MoveDown();
		if (dropPressed) _gameManager.DropShape();

		if (rotatePressed || leftPressed || rightPressed || downPressed) {
			_nextMove = 0.085;
		}

		if (dropPressed) {
			_nextMove = 0.3;
		}

		Draw(_gameManager);

		if (pausePressed && !_isPaused) {
			PauseInput_Pressed();
			_isPaused = true;
			return;
		}
	}

	private ColorRect[,] ConstructGameField(Grid grid) {
		_shapeControls = new ColorRect[grid.Rows, grid.Columns];
		const int cellSize = 40;

		for (int rows = 0; rows < grid.Rows; rows++) {
			for (int cols = 0;  cols < grid.Columns; cols++) {
				ColorRect shapeControl = new();
				shapeControl.Color = _shapeColors[0];
				shapeControl.Size = new Vector2(cellSize, cellSize);
				shapeControl.Position = new Vector2(cols * cellSize, (rows - 2) * cellSize);
				_gameCanvas.AddChild(shapeControl);
				_shapeControls[rows, cols] = shapeControl;
			}
		}

		return _shapeControls;
	}

	private ColorRect[,] ConstructNextShapeField() {
		_nextShapeControls = new ColorRect[4, 4];
		const int cellSize = 20;

		for (int rows = 0; rows < 4; rows++) {
			for (int cols = 0; cols < 4; cols++) {
				ColorRect shapeControl = new();
				shapeControl.Color = _shapeColors[0];
				shapeControl.Size = new Vector2(cellSize, cellSize);
				shapeControl.Position = new Vector2(cols * cellSize, rows * cellSize);
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

	private void DrawShape(Shape shape) {
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

	private void DrawGhost(Shape shape) {
		int dropDistance = _gameManager.ShapeDropDistance();
		for (int row = 0; row < shape.CurrentRows; row++) {
			for (int col = 0; col < shape.CurrentCols; col++) {
				if (shape.CurrentShapeMatrix[row, col] == 1) {
					Color ghostColor = _shapeColors[shape.Id];
					ghostColor.A = 0.25f;
					_shapeControls[(int)shape.Pos.X + row + dropDistance, (int)shape.Pos.Y + col].Color = ghostColor;
				}
			}
		}
	}

	private new void Draw(Manager gameManager) {
		DrawGrid(gameManager.Grid);
		if (!_isHard) DrawGhost(gameManager.ActiveShape);
		DrawShape(gameManager.ActiveShape);
		DrawNext(gameManager.Queue.NextShape);
		_lineText.Text = $"SOROK: {gameManager.Score:0000}";
	}

	private async Task GameLoop() {
		do {
			_isInMainLoop = true;
			Draw(_gameManager);
			float delay = Math.Max(_minDelay, _maxDelay - (_gameManager.Score * _delayDecrease));

			await Task.Delay(TimeSpan.FromMilliseconds(delay));

			if (!_gameRunning) continue;

			_gameManager.MoveDown();
		} while (!_gameManager.IsGameOver && _gameRunning);

		_isInMainLoop = false;

		if (!_gameManager.IsGameOver) return;

		_menu.Visible = true;
	}

	public async void Play_Pressed(string difficulty) {
		_menu.Visible = false;
		_gameRunning = true;
		switch (difficulty) {
			case "Weak":
				_minDelay = 250;
				_maxDelay = 1000;
				_delayDecrease = 20;
				break;
			case "Med":
				_minDelay = 175;
				_maxDelay = 850;
				_delayDecrease = 30;
				break;
			case "Hard":
				_minDelay = 100;
				_maxDelay = 650;
				_delayDecrease = 50;
				_isHard = true;
				break;
		}

		_gameManager = new Manager();

        if (!_isInMainLoop) await GameLoop();
	}

	private void PauseInput_Pressed() {
		_pauseMenu.Visible = true;
		_gameRunning = false;
	}

	public async void PauseContinue_Pressed() {
		_pauseMenu.Visible = false;
		_gameRunning = true;
		if (!_isInMainLoop) await GameLoop();
	}

	public void PauseReplay_Pressed() {
		_pauseMenu.Visible = false;
		_gameRunning = false;
		_menu.Visible = true;
	}

	public void PauseExit_Pressed() {
		GetTree().Quit();
	}
}
