using Godot;

public partial class Menu : Control {
    public static Menu Instance;
    public string Selected { get; private set; }

    public override void _Ready() {
        Instance = this;

        Godot.Collections.Array<Node> gyerekek = GetChildren();
        foreach (Node n in gyerekek) {
            if (n.Name.ToString()[0] == 'S' && n is Button btn) {
                //GD.Print($"\t{n.Name.ToString()[0]}");
                btn.Pressed += () => DiffButton_Pressed(n.Name.ToString()[6..]);
            }
        }

        GetNode<Button>("Play").Pressed += () => GameField.Instance.Play_Pressed(Selected);

        UpdateColors();
    }

    void UpdateColors() {
        bool selectionExists = false;

        foreach (Node n in GetChildren()) {
            if (n.Name.ToString()[0] == 'S' && n is Button btn) {
                if (btn.Name == $"Select{Selected}") {
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
        Selected = start;
        UpdateColors();
    }
}
