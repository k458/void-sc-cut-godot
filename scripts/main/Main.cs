using Godot;
using voidsccut.scripts.client.model;
using voidsccut.scripts.messageService;
using voidsccut.scripts.screens;
using voidsccut.scripts.shared;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.main;

public partial class Main : Node2D, IMain, IMessageReceiver
{
	 public bool IsPlayerControlAllowed { get; private set; }
	 private ScreenManager _screenManager;
	 
	 private IProcessable _playerActionsNonIntrusive;
	 private IProcessable _playerActions;
	 private IProcessable _cityController;
	 private IProcessable _combatController;
	 
	 private bool _isAuthorizationBusy = false;
	 
	public override void _Ready()
	{
		//init
		_screenManager = GetNode<ScreenManager>("ScreenManager");
		
		Game.SetMain(this);
		Game.ClientService.Init();
		Game.MessageManager.AddMessageReceiver(this);
		Game.SetState(GameState.Login);
	}
	
	public override void _Process(double delta)
	{
		float deltaFloat = (float)delta;
		TryChangeScreen();
		
		Game.ClientService.Processable.Process(deltaFloat);
		if (!Game.ClientService.Processable.IsFinished) return;
		
		_screenManager.CurrentScreen.Process(deltaFloat);
		if (!_screenManager.CurrentScreen.IsFinished) return;
		 
		
		 // _playerActionsNonIntrusive.Process(deltaFloat);
		 // if (!_playerActionsNonIntrusive.IsFinished) return;
		
		 
		
		 return;
		 switch (Game.State)
		 {
		 	case GameState.None:
		 	{
		 		break;
		 	}
		 	case GameState.City:
		 	{
		 		_cityController.Process(deltaFloat);
		 		if (!_cityController.IsFinished) return;
		 		break;
		 	}
		 	case GameState.Combat:
		 	{
		 		_combatController.Process(deltaFloat);
		 		if (!_combatController.IsFinished) return;
		 		break;
		 	}
		 	default:
		 		break;
		}
		
		 _playerActions.Process(deltaFloat);
		 if (!_playerActions.IsFinished) return;
		
		
	}

	public void TryChangeScreen()
	{
		GameState state = Game.State;
		switch (state)
		{
			case GameState.Login:
				_screenManager.SetScreen(ScreenType.Login);
				break;
			case GameState.PlaceHolder:
				_screenManager.SetScreen(ScreenType.Placeholder);
				break;
		}
	}
	public void Log(string s) => GD.Print(s);
	public void SetIsPlayerControlAllowed(bool allowed) => IsPlayerControlAllowed = allowed;

	public void Login(string name, string password)
	{
		if(_isAuthorizationBusy)return;
		_isAuthorizationBusy = true;
		UserNamePassword userNamePassword = new UserNamePassword();
		userNamePassword.Name = name;
		userNamePassword.Password = password;
		//Game.MessageManager.AddMessageReceiver(this, MessageType.ServerAuthorized);
		Game.ClientService.AddRequest(RequestType.Login, userNamePassword);
	}

	public void CreateAccount(string name, string password)
	{
		if(_isAuthorizationBusy)return;
		_isAuthorizationBusy = true;
		UserNamePassword userNamePassword = new UserNamePassword();
		userNamePassword.Name = name;
		userNamePassword.Password = password;
		//Game.MessageManager.AddMessageReceiver(this, MessageType.ServerAuthorized);
		Game.ClientService.AddRequest(RequestType.CreateUser, userNamePassword);
	}

	public void Message(MessageType type)
	{
		switch (type)
		{
			case MessageType.ClientAuthorized:
				Game.SetState(GameState.PlaceHolder);
				_isAuthorizationBusy = false;
				break;
			case MessageType.ClientAuthorizationFailed:
				Game.SetState(GameState.Login);
				_isAuthorizationBusy = false;
				break;
		}
	}

	public void Logout()
	{
		Game.SetState(GameState.Login);
		Game.ClientService.Logout();
	}
}
