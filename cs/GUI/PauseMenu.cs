using Godot;
using System;

public partial class PauseMenu : Control {
	public void _Init() {
        GetNode<Button>("ContinueButton").Pressed += GameField.Instance.PauseContinue_Pressed;
        GetNode<Button>("ExitButton").Pressed += GameField.Instance.PauseExit_Pressed;
	}
}
