using Godot;
using voidsccut.scripts.screens.ui;
using voidsccut.scripts.screens.ui.buttons;
using voidsccut.scripts.screens.ui.labels;

namespace voidsccut.scripts.screens.loginScreen;

public partial class LoginScreen : Screen
{
    private AuthorizanionType _authorizanionType = AuthorizanionType.None;
    private TextEdit _loginText;
    private TextEdit _passwordText;
    private TimedTextLabel _timedTextLabel;
    private string _name;
    private string _password;
    
    public override void _Ready()
    {
        UiRoot.GetNode<ScreenButton>("LoginButton").SetScreen(this);
        UiRoot.GetNode<ScreenButton>("CreateAccountButton").SetScreen(this);
        _loginText = UiRoot.GetNode<TextEdit>("NameText");
        _passwordText = UiRoot.GetNode<TextEdit>("PasswordText");
        _timedTextLabel = UiRoot.GetNode<screens.ui.labels.TimedTextLabel>("ErrorLabel");
    }

    public override void Process(float deltaTime)
    {
        if (_authorizanionType == AuthorizanionType.Login)
        {
            Game.Main.Login(_name, _password);
            _authorizanionType = AuthorizanionType.None;
            IsFinished = false;
        }
        else if (_authorizanionType == AuthorizanionType.CreateNewAccount)
        {
            Game.Main.CreateAccount(_name, _password);
            _authorizanionType = AuthorizanionType.None;
            IsFinished = false;
        }
        else
        {
            IsFinished = true;
        }
    }
    
    public override void Login()
    {
        if (UpdateNamePassword()) _authorizanionType = AuthorizanionType.Login;
    }

    public override void CreateAccount()
    {
        if (UpdateNamePassword()) _authorizanionType = AuthorizanionType.CreateNewAccount;
    }

    private bool UpdateNamePassword()
    {
        _name = _loginText.Text;
        _password = _passwordText.Text;
        bool valid = _name != null && 
                     _name.Length >= 5 &&
                     _password != null &&
                     _password.Length >= 5;
        if (!valid) _timedTextLabel.ShowTextFor("Name or Password is too short", 3f);
        return valid;
    }

    
}