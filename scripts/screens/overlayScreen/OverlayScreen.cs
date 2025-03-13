using voidsccut.scripts.screens.loginScreen;
using TimedTextLabel = voidsccut.scripts.screens.ui.labels.TimedTextLabel;

namespace voidsccut.scripts.screens.overlayScreen;

public partial class OverlayScreen : Screen
{
    private const float LogDuration = 5f;
    
    private TimedTextLabel _logText;
    
    public override void _Ready()
    {
        _logText = UiRoot.GetNode<TimedTextLabel>("LogLabel");
    }

    public override void Process(float deltaTime)
    {
        IsFinished = true;
    }
    
    public void Log(string s, float duration)
    {
        _logText.ShowTextFor(s, duration);
    }
}