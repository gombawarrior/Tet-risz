using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

public partial class PopMenu : Control {
    private InputEventKey _key;
    private Label _actionLabel;
    private Label _inputLabel;
    private List<string> _keyList;
    private InputEventKey EventKey(string action) => (InputEventKey)InputMap.ActionGetEvents(action)[0];

    public static PopMenu Instance;
    public string Action {
        get => _actionLabel.Text;
        set => _actionLabel.Text = value;
    }
    public InputEventKey Key {
        get => _key;
        set {
            _key = value;
            _inputLabel.Text = _key.Keycode.ToString();
        }
    }

    public void _Init() {
        Button okButton = GetNode<Button>("OK");
        Button cancelButton = GetNode<Button>("Cancel");
        _actionLabel = GetNode<Label>("ActionLabel");
        _inputLabel = GetNode<Label>("InputLabel");

        okButton.Pressed += () => ControlMenu.Instance.PopOk_Pressed(Key);
        cancelButton.Pressed += ControlMenu.Instance.PopCancel;
        _keyList = GetKeyList();
    }

    public override void _Input(InputEvent @event) {
        if (Visible == false) return;
        if (@event is InputEventKey key) {
            InputEventKey pauseKey = (InputEventKey)InputMap.ActionGetEvents("Pause")[0];

            if (key.Keycode == pauseKey.Keycode) ControlMenu.Instance.PopCancel();
            if (_keyList.Contains(key.Keycode.ToString())) return;

            Key = key;
        }
    }

    private List<string> GetKeyList() {
        List<string> list = new();
        var actions = InputMap.GetActions().ToList();
        for (int i = 76; i < actions.Count; i++) {
            list.Add(EventKey(actions[i]).Keycode.ToString());
        }

        return list;
    }
}
