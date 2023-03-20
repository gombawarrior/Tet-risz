using Godot;

public partial class Menu : Control {
    public static Menu Instance;
    private string _selected;

    public override void _Ready() {
        Instance = this;
        
        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'S' && n is Button btn) {
                btn.Pressed += () => DiffButton_Pressed(n.Name.ToString()[6..]);
            }
        }

        GetNode<Button>("Play").Pressed += () => GameField.Instance.Play_Pressed(_selected);

        GetNode<Button>("Config").Pressed += GameField.Instance.ConfigButton_Pressed;
        GetNode<Button>("Leaderboard").Pressed += GameField.Instance.LeaderButton_Pressed;

        UpdateColors();
    }

    void UpdateColors() {
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
}
