using Godot;
using voidsccut.scripts.screens.ui;

namespace voidsccut.scripts.screens.loginScreen.ui;

public partial class CreateAccountButton : ScreenButton
{
    public override void _Pressed() => Screen.CreateAccount();
}