using Godot;
using System.Collections.Generic;

public class OShape : ShapeControl {
    protected override List<List<int>> ShapeMatrix => new List<List<int>> {
        {new List<int> {1, 1}},
        {new List<int> {1, 1}}
    };
    protected override Vector2 StartPos => new Vector2(0, 4);
    public override int Id => 2;
}