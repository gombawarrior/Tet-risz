using Godot;
using Tetrisz;

public class JShape : Shape {
    protected override int[,] ShapeMatrix => new int[2, 3] {
        {1, 1, 1},
        {0, 0, 1}
    };
    protected override Vector2 StartPos => new(0, 3);
    public override int Id => 5;
}