using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Godot;
using voidsccut.scripts.client.model;
using voidsccut.scripts.shared;
using voidsccut.scripts.shared.serverTypes;
using HttpClient = Godot.HttpClient;

namespace voidsccut.scripts.main;

public partial class Main : Node2D, IMain, IMainManipulator
{
	public IMainManipulator Manipulator => this;
	
	 public bool IsPlayerControlAllowed { get; private set; }
	
	 private IProcessable _playerActionsNonIntrusive;
	 private IProcessable _playerActions;
	 private IProcessable _cityController;
	 private IProcessable _combatController;

	 public Main()
	 {
		 Game.SetMain(this);
	 }
	 
	public override void _Ready()
	{
		//init
		//test
		UserNamePassword userNamePassword = new UserNamePassword();
		userNamePassword.Name = "admin";
		userNamePassword.Password = "password";
		Game.ClientService.AddRequest(RequestType.Login, userNamePassword);
	}
	
	public override void _Process(double delta)
	{
		 float deltaFloat = (float)delta;
		
		 // _playerActionsNonIntrusive.Process(deltaFloat);
		 // if (!_playerActionsNonIntrusive.IsFinished) return;
		
		 Game.ClientService.Processable.Process(deltaFloat);
		 if (!Game.ClientService.Processable.IsFinished) return;
		
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

	public void Log(string s) => GD.Print(s);
	public void SetIsPlayerControlAllowed(bool allowed) => IsPlayerControlAllowed = allowed; 
}
