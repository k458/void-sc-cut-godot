using Godot;
using voidsccut.scripts.screens.ui;

namespace voidsccut.scripts.screens.placeholderScreen;

public partial class PlaceholderScreen : Screen
{
    private bool _logoutRequested = false;
    
    public override void _Ready()
    {
        UiRoot.GetNode<ScreenButton>("LogoutButton").SetScreen(this);
    }

    public override void Process(float deltaTime)
    {
        if (_logoutRequested)
        {
            _logoutRequested = false;
            Game.Main.Logout();
            IsFinished = false;
        }
        else
        {
            IsFinished = true;
        }
    }
    public override void Logout()
    {
        _logoutRequested = true;
    }
}