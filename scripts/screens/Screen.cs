using Godot;
using voidsccut.scripts.shared;

namespace voidsccut.scripts.screens;

public abstract partial class Screen : Node2D, IProcessable
{
    [Export]
    public Control UiRoot {get; set;}
    public abstract void Process(float deltaTime);
    public bool IsFinished { get; protected set; }

    public void HideScreen()
    {
        this.Hide();
        UiRoot.Hide();
    }

    public void ShowScreen()
    {
        this.Show();
        UiRoot.Show();
    }
    public virtual void Logout()
    {
    }
    public virtual void Login()
    {
    }
    public virtual void CreateAccount()
    {
    }
}