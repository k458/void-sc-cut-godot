using System.Collections.Generic;
using Godot;
using voidsccut.scripts.screens.loginScreen;
using voidsccut.scripts.screens.overlayScreen;
using voidsccut.scripts.screens.placeholderScreen;

namespace voidsccut.scripts.screens;

public partial class ScreenManager : Node2D
{
    public Screen CurrentScreen { get; private set; }
    public OverlayScreen OverlayScreen { get; private set; }
    
    private Screen _loginScreen;
    private Screen _placeholderScreen;
    private readonly List<Screen> _screens = [];
    private ScreenType _currentScreenType;

    public override void _Ready()
    {
        OverlayScreen = GetNode<OverlayScreen>("OverlayScreen");
        
        _loginScreen = GetNode<LoginScreen>("LoginScreen");
        _screens.Add(_loginScreen);
        
        _placeholderScreen = GetNode<PlaceholderScreen>("PlaceholderScreen");
        _screens.Add(_placeholderScreen);
        
        HideAllScreens();
    }

    public void SetScreen(ScreenType screenType)
    {
        if (_currentScreenType == screenType) return;
        _currentScreenType = screenType;
        HideAllScreens();
        switch (screenType)
        {
            case ScreenType.Login:
                _loginScreen.ShowScreen();
                CurrentScreen = _loginScreen;
                break;
            case ScreenType.Placeholder:
            {
                _placeholderScreen.ShowScreen();
                CurrentScreen = _placeholderScreen;
                break;
            }
        }
    }

    private void HideAllScreens()
    {
        foreach (var screen in _screens)
        {
            screen.HideScreen();
        }
    }
}