using Godot;
using System;

public partial class ControlMenu : Control {

    public override void _Ready() {

        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'B' && n is Button btn) {
                btn.Pressed += () => ControlButton_Pressed(btn.Name.ToString()[6..]);
            }
        }
        
    }

    private void ControlButton_Pressed(string key) {

    }
}
