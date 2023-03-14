using System;

public partial class Queue {
    Random rand = new();
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
    private Shape RandomShape => Shapes[rand.Next(Shapes.Length)];
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
