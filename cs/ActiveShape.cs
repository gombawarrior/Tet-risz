using Godot;
using System;

public class ActiveShape : KinematicBody2D {
    Random rng = new Random();
    bool IsActive = false;
    PackedScene[] shapes;
    KinematicBody2D currentShape = null;
    KinematicBody2D nextShape = null;
    public override void _Ready() {
        shapes = new PackedScene[6];
        for (int i = 0; i < shapes.Length; i++) {
            shapes[i] = (PackedScene)ResourceLoader.Load($"res://Shapes/Shape{i}.tscn");
        }
    }

    public override void _Process(float delta) {
        if (!IsActive) {
            ChooseShape();
            IsActive = true;
        }
    }

    public void ChooseShape() {
        if (nextShape is null) nextShape = (KinematicBody2D)shapes[rng.Next(0, shapes.Length)].Instance();
        currentShape = nextShape;
        nextShape = (KinematicBody2D)shapes[rng.Next(0, shapes.Length)].Instance();
    }
}
