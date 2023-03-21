using Godot;
using System;

public partial class PopMenu : Control {
    public string Action;
    public InputEventKey Key;
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

         okButton.Pressed += () => ControlMenu.Instance.Ok_Pressed(Key);
         _popMenu.VisibilityChanged += Visibility_Changed;
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventKey key) {
            Key = key;
            _inputLabel.Text = Key.Keycode.ToString();
        }
    }

    public void Visibility_Changed() {
        _actionLabel.Text = Action;
        _inputLabel.Text = Key.Keycode.ToString();
    }
}
