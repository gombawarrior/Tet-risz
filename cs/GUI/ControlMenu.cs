using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class ControlMenu : Control {
    private string _action;
    private Control _popup;
    private Godot.Collections.Array<StringName> _actionArray;
    public static ControlMenu Instance;

    public override void _Ready() {
        Instance = this;

        _popup = GetNode<Control>("PopupMenu");
        _actionArray = InputMap.GetActions();
        
        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'B' && n is Button btn) {
                string action = btn.Name.ToString()[6..];
                var eventKey = (InputEventKey)InputMap.ActionGetEvents(action)[0];
                btn.Text = eventKey.Keycode.ToString();
                btn.Pressed += () => ControlButton_Pressed(_actionArray.First(e => e == action), eventKey.Keycode.ToString());
                
            }
        }
        
    }

    private void ControlButton_Pressed(string action, string actionKey) {
        _action = action;
        PopMenu.Instance.Action = _action;
        PopMenu.Instance.Key = actionKey;
        GD.Print(_action);
        GD.Print(actionKey);
        _popup.Visible = true;
    }

    public void Ok_Pressed(string input) {
        _popup.Visible = false;

    }
}
