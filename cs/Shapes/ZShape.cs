using Godot;
using System.Collections.Generic;

public class ZShape : ShapeControl {
    protected override List<List<int>> ShapeMatrix => new List<List<int>> {
        {new List<int> {1, 1, 0, 0}},
        {new List<int> {0, 0, 1, 1}}
    };
    protected override Vector2 StartPos => new Vector2(0, 3);
    public override int Id => 4;
}