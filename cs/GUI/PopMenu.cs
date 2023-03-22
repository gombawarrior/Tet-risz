using Godot;
using System;

public partial class PopMenu : Control {
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
    private InputEventKey _key = null;
    private Label _actionLabel;
    private Label _inputLabel;
    private Control _popMenu;
    public static PopMenu Instance;

    public override void _Ready() {
        Instance = this;
        GD.Print("miva");
        Button okButton = GetNode<Button>("OK");
         _actionLabel = GetNode<Label>("ActionLabel");
         _inputLabel = GetNode<Label>("InputLabel");
         _popMenu = GetNode<Control>(".");

         okButton.Pressed += () => ControlMenu.Instance.PopOk_Pressed(Key);
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventKey key) {
            Key = key;
        }
    }
}
