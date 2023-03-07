using Godot;

public class TShape : Shape {
    protected override int[,] ShapeMatrix => new int[2, 3] {
        {0, 1, 0},
        {1, 1, 1}
    };
    protected override Vector2 StartPos => new Vector2(0, 3);
    public override int Id => 7;
}