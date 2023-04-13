using Godot;

public partial class ControlMenu : Control {
    private string _action;
    private PopMenu _popup;
    private Button _okButton;
    private InputEventKey EventKey(string action) => (InputEventKey)InputMap.ActionGetEvents(action)[0];
    public static ControlMenu Instance;

    public override void _Ready() {
        Instance = this;

        _popup = GetNode<PopMenu>("PopupMenu");
        _okButton = GetNode<Button>("OK");
        
        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'B' && n is Button btn) {
                string action = btn.Name.ToString()[6..];
                btn.Text = EventKey(action).Keycode.ToString();
                btn.Pressed += () => ControlButton_Pressed(action);
            }
        }

        _okButton.Pressed += Menu.Instance.ConfigOkButton_Pressed;
        _popup._Init();
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
        _popup.Action = _action;
        _popup.Key = EventKey(_action);
        FocusMode = FocusModeEnum.None;
        _popup.Visible = true;
        _okButton.Disabled = true;
    }

    public void PopOk_Pressed(InputEventKey input) {
        _popup.Visible = false;
        _okButton.Disabled = false;
        FocusMode = FocusModeEnum.All;
        InputMap.ActionEraseEvents(_action);
        InputMap.ActionAddEvent(_action, _popup.Key);
        _popup.KeyList = _popup.GetKeyList();

        UpdateKeys();
    }

    public void PopCancel() {
        _popup.Visible = false;
        _okButton.Disabled = false;
    }
}
