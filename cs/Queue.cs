﻿using System;

public class Queue {
    Random rand = new Random();
    public ShapeControl[] Shapes = { 
        new IShape(),
        new JShape(),
        new LShape(),
        new OShape(),
        new SShape(),
        new ZShape()
    };
    public ShapeControl NextShape { get; private set; }
    private ShapeControl RandomShape => Shapes[rand.Next(Shapes.Length)];
}
