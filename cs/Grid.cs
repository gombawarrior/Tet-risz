using Godot;
using System;

public class Grid : TileMap {
    float tileSize;
    Vector2 gridSize = new Vector2(10, 20);
    int[,] grid = new int[10, 20];

    public override void _Ready() {
        var activeShape = GD.Load("");
        tileSize = CellSize.x;
    }

	public override void _Process(float delta) {
		
	}
}
