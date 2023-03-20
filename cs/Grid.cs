using System.Linq;

public partial class Grid {
	public int Rows { get; }
	public int Columns { get; }
    private readonly int[,] _grid;

	public int this[int row, int col] {
		get => _grid[row, col];
		set => _grid[row, col] = value;
	}
	
	public Grid(int rows, int cols) {
		Rows = rows;
		Columns = cols;
		_grid = new int[Rows, Columns];
	}

	private bool IsInside(int row, int col) => row >= 0 && row < Rows && col >= 0 && col < Columns;

	public bool IsEmpty(int row, int col) => IsInside(row, col) && _grid[row, col] == 0;

	public bool IsFullRow(int row) => Enumerable.Range(0, Columns).All(col => _grid[row, col] != 0);

	public bool IsEmptyRow(int row) => Enumerable.Range(0, Columns).All(col => _grid[row, col] == 0);

	private void ClearRow(int row) {
		for (int col = 0;  col < Columns; col++) {
			_grid[row, col] = 0;
		}
	}

	private void MoveRowDown(int row, int distance) {
		for (int col = 0; col < Columns; col++) {
			_grid[row + distance, col] = _grid[row, col];
            //grid[row, col] = 0;
        }

		ClearRow(row);
	}

	public int ClearFullRows() {
		int clearedRows = 0;
		for (int row = Rows - 1; row >= 0; row--) {
			if (IsFullRow(row)) {
				ClearRow(row);
				clearedRows++;
			}
			else if (clearedRows > 0) {
				MoveRowDown(row, clearedRows);
			}
		}
		return clearedRows;
	}
}
