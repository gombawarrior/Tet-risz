using Godot;
using System;
using System.Collections.Generic;

public partial class ControlMenu : Control {
    private string _action;
    private Control _popup;
    private InputEventKey EventKey(string action) => (InputEventKey)InputMap.ActionGetEvents(action)[0];
    public static ControlMenu Instance;

    public override void _Ready() {
        Instance = this;

        _popup = GetNode<Control>("PopupMenu");
        
        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'B' && n is Button btn) {
                string action = btn.Name.ToString()[6..];
                btn.Text = EventKey(action).Keycode.ToString();
                btn.Pressed += () => ControlButton_Pressed(action);
            }
        }
    }

    private void UpdateKeys() {
        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'B' && n is Button btn) {
                string action = btn.Name.ToString()[6..];
                btn.Text = EventKey(action).Keycode.ToString();
            }
        }
    }

    private void ControlButton_Pressed(string action) {
        _action = action;
        PopMenu.Instance.Action = _action;
        PopMenu.Instance.Key = EventKey(_action);
        _popup.Visible = true;
    }

    public void Ok_Pressed(InputEventKey input) {
        _popup.Visible = false;
        InputMap.ActionEraseEvents(_action);
        InputMap.ActionAddEvent(_action, PopMenu.Instance.Key);

        UpdateKeys();
    }
}
