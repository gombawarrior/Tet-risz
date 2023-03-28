using System;

namespace Tetrisz;

public partial class Queue {
    private readonly Random _rand = new();
    public Shape[] Shapes = { 
        new IShape(),
        new JShape(),
        new LShape(),
        new OShape(),
        new SShape(),
        new ZShape(),
        new TShape()
    };
    public Shape NextShape { get; private set; }
    private Shape RandomShape => Shapes[_rand.Next(Shapes.Length)];
    //private Shape RandomShape => Shapes[0];

    public Queue() {
        NextShape = RandomShape;
    }

    public Shape UpdateShape() {
        Shape shape = NextShape;
        NextShape = RandomShape;
        return shape;
    }
}
