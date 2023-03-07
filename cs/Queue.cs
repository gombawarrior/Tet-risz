using System;

public class Queue {
    Random rand = new Random();
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

    public Queue() {
        NextShape = RandomShape;
    }

    public Shape UpdateShape() {
        Shape shape = NextShape;
        NextShape = RandomShape;
        return shape;
    }
}
