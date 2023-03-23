using Godot;
using Tetrisz;

public class OShape : Shape {
    protected override int[,] ShapeMatrix => new int[2, 2] {
        {1, 1},
        {1, 1}
    };
    protected override Vector2 StartPos => new(0, 4);
    public override int Id => 2;
}