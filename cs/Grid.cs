using System.Linq;

public class Grid {
	public int Rows { get; }
	public int Columns { get; }
	readonly int[,] grid;

	public int this[int row, int col] {
		get => grid[row, col];
		set => grid[row, col] = value;
	}
	
	public Grid(int rows, int cols) {
		Rows = rows;
		Columns = cols;
		grid = new int[cols, Rows];
	}

	private bool IsInside(int row, int col) => row >= 0 && row < Rows && col >= 0 && col < Columns;

	public bool IsEmpty(int row, int col) => IsInside(row, col) && grid[row, col] == 0;

	public bool IsFullRow(int row) => Enumerable.Range(0, Columns).All(col => grid[row, col] != 0);

	public bool IsEmptyRow(int row) => Enumerable.Range(0, Columns).All(col => grid[row, col] == 0);

	private void ClearRow(int row) {
		for (int col = 0;  col < Columns; col++) {
			grid[row, col] = 0;
		}
	}

	private void MoveRowDown(int row, int distance) {
		for (int col = 0; col < Columns; col++) {
			grid[row + distance, col] = grid[row, col];
		}
		ClearRow(row);
	}

	public int ClearFullRows() {
		int clearedRows = 0;
		for (int row = Rows - 1; row >= 0 && !IsEmptyRow(row); row--) {
			if (IsFullRow(row)) {
				ClearRow(row);
				clearedRows++;
			}
			if (clearedRows > 0) {
				MoveRowDown(row, clearedRows);
			}
		}
		return clearedRows;
	}
}
