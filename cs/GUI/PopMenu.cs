using Godot;
using System;

public partial class PopMenu : Control {
    public string Action, Key = "";
    private Label _actionLabel;
    private Label _inputLabel;
    public static PopMenu Instance;

    public override void _Ready() {
        Instance = this;
        GD.Print("miva");
        Button okButton = GetNode<Button>("OK");
         _actionLabel = GetNode<Label>("ActionLabel");
         _inputLabel = GetNode<Label>("InputLabel");

         //okButton.Pressed += () => ControlMenu.Instance.Ok_Pressed(Key);
    }

    public override void _Input(InputEvent @event) {
        
    }

    public void Visibility_Changed() {
        _actionLabel.Text = Action;
        _inputLabel.Text = Key;
    }
}
