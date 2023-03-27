using Tetrisz;

namespace Tet_RiszTest;

[TestClass]
public class GridTest {
    [TestMethod]
    public void IsInside_IfRowIsNegativeReturnsFalse() {
        Grid grid = new(20, 20);
        const int row = -10;
        const int col = 10;
        
        bool actual = grid.IsEmpty(row, col);
        Assert.IsFalse(actual, "Position was not checked correctly");
    }

    [TestMethod]
    public void IsInside_IfColIsNegativeReturnsFalse() {
        Grid grid = new(20, 20);
        const int row = 10;
        const int col = -10;

        bool actual = grid.IsEmpty(row, col);
        Assert.IsFalse(actual, "Position was not checked correctly");
    }
}