using Godot;

public partial class Menu : Control {
    public static Menu Instance;
    private string _selected;
    private Control _configMenu;

    public override void _Ready() {
        Instance = this;
        
        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'S' && n is Button btn) {
                btn.Pressed += () => DiffButton_Pressed(n.Name.ToString()[6..]);
            }
        }

        _configMenu = GetNode<Control>("../ControlMenu");

        GetNode<Button>("Play").Pressed += () => GameField.Instance.Play_Pressed(_selected);

        GetNode<Button>("Config").Pressed += ConfigButton_Pressed;
        GetNode<Button>("Exit").Pressed += ExitButton_Pressed;

        UpdateColors();
    }

    private void UpdateColors() {
        bool selectionExists = false;

        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'S' && n is Button btn) {
                if (btn.Name == $"Select{_selected}") {
                    selectionExists = true;
                    btn.Modulate = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                }
                else {
                    btn.Modulate = new Color(1.0f, 1.0f, 1.0f, 0.75f);
                }
            }
        }

        GetNode<Button>("Play").Disabled = !selectionExists;
    }

    private void DiffButton_Pressed(string start) {
        _selected = start;
        UpdateColors();
    }

    private void ExitButton_Pressed() {
        GetTree().Quit();
    }

    public void ConfigButton_Pressed() {
        Visible = false;
        _configMenu.Visible = true;
    }

    public void ConfigOkButton_Pressed() {
        _configMenu.Visible = false;
        Visible = true;
    }
}
