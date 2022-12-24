using Godot;
using System;

public class Shapes : Node2D {
    Node2D shape;
    float y;
    float x;
    protected Vector2[,] rotationMatrix = new Vector2[4, 4];
    public override void _Ready() {
        shape = (Node2D)GetChild(0);
        y = 150;
        x = GetViewport().GetVisibleRect().Size.x / 2;
        shape.Position = new Vector2(x, y);
    }

    public override void _Process(float delta) {
        //shape.Position = new Vector2(1, y += delta*10);
        bool leftPressed = Input.IsActionPressed("left");
        bool rightPressed = Input.IsActionPressed("right");
        bool downPressed = Input.IsActionPressed("down");
        if (downPressed) {
            shape.Position = new Vector2(x, y += delta * 100);
        }
    }
}
