using Godot;
using System;

public class Shape2 : Shapes {
    public override void _Ready() {
        rotationMatrix = new Vector2[,] { 
            {new Vector2(0f, 0f), new Vector2(-70f, 0f), new Vector2(-70f, -70f), new Vector2(-70f, -140)},
            {new Vector2(-70f, -70f), new Vector2(0f, -70f), new Vector2(0f, -70f), new Vector2(70f, -70f)},
            {new Vector2(-70f, -70), new Vector2(-70f, -70f), new Vector2(0f, 0f), new Vector2(0f, 70f)},
            {new Vector2(0f, -70f), new Vector2(0f, 0f), new Vector2(-70f, 0f), new Vector2(-140f, 0f)}
        };
    }
}
