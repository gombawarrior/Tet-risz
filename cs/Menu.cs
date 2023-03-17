using Godot;
using System;
using System.Linq;

public partial class Menu : Control {
    public string selected;

    public override void _Ready() {
        Godot.Collections.Array<Node> gyerekek = GetChildren();
        foreach (Node n in gyerekek) {
            if (n.SceneFilePath.Split('/').Last()[0] == 'S' && n is Button btn) {
                btn.Pressed += () => Button_Pressed(btn.SceneFilePath.Split('/').Last()[6..]);
            }
        }
    }

    private void Button_Pressed(string start) {
        GD.Print(start);
    }
}
