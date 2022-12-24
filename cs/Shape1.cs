using Godot;
using System;

public class Shape1 : Shapes {
    public override void _Ready() {
        rotationMatrix = new Vector2[,] { 
            {new Vector2(-70f, 0f), new Vector2(-70f, -70f), new Vector2(0f, 0f), new Vector2(0f, -70f)},
            {new Vector2(-70f, 0f), new Vector2(-70f, -70f), new Vector2(0f, 0f), new Vector2(0f, -70f)},
            {new Vector2(-70f, 0f), new Vector2(-70f, -70f), new Vector2(0f, 0f), new Vector2(0f, -70f)},
            {new Vector2(-70f, 0f), new Vector2(-70f, -70f), new Vector2(0f, 0f), new Vector2(0f, -70f)}
        };
    }
}
