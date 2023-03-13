using Godot;

public partial class IShape : Shape {
    protected override int[,] ShapeMatrix => new int[1, 4] {
        {1, 1, 1, 1}
    };
    protected override Vector2 StartPos => new Vector2(0, 3);
    public override int Id => 1;
}