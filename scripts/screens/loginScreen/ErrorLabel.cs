using System;
using Godot;

namespace voidsccut.scripts.screens.loginScreen;

public partial class ErrorLabel : Label
{
    private float _timeLeft;
    public override void _Process(double delta)
    {
        if (!this.Visible) return;
        float deltaTime = (float)delta;
        _timeLeft -= deltaTime;
        if (_timeLeft < 0)
        {
            Text = "";
            Hide();
        }
    }
    public void ShowTextFor(String text, float seconds)
    {
        Text = text;
        _timeLeft = seconds;
        Show();
    }
}