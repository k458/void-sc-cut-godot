using Godot;

namespace voidsccut.scripts.screens.ui;

public abstract partial class ScreenButton : Button
{
    protected Screen Screen { get; private set; }
    public void SetScreen(Screen screen) => Screen = screen;

}