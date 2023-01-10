using Godot;
using System.Collections.Generic;

public class IShape : ShapeControl {
    protected override List<List<int>> ShapeMatrix => new List<List<int>> {
        {new List<int> {1, 1, 1, 1}}
    };
    protected override Vector2 StartPos => new Vector2(-1, 3);
    public override int Id => 1;
}