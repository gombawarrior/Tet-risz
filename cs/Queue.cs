using System;

public class Queue {
    Random rand = new Random();
    public ShapeControl[] Shapes = { 
        new IShape(),
        new JShape(),
        new LShape(),
        new OShape(),
        new SShape(),
        new ZShape(),
        new TShape()
    };
    public ShapeControl NextShape { get; private set; }
    private ShapeControl RandomShape => Shapes[rand.Next(Shapes.Length)];

    public Queue() {
        NextShape = RandomShape;
    }

    public ShapeControl UpdateShape() {
        ShapeControl shape = NextShape;
        NextShape = RandomShape;
        return shape;
    }
}
