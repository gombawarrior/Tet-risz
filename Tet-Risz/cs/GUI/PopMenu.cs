using Godot;

public partial class PopMenu : Control {
    private InputEventKey _key;
    private Label _actionLabel;
    private Label _inputLabel;

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

    public override void _Ready() {
        Instance = this;

        Button okButton = GetNode<Button>("OK");
         _actionLabel = GetNode<Label>("ActionLabel");
         _inputLabel = GetNode<Label>("InputLabel");

         okButton.Pressed += () => ControlMenu.Instance.PopOk_Pressed(Key);
    }

    public override void _Input(InputEvent @event) {
        if (@event is InputEventKey key) {
            Key = key;
        }
    }
}
