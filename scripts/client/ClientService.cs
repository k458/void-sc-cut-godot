using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using GodotPlugins.Game;
using voidsccut.scripts.client.model;
using voidsccut.scripts.client.model.requests;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.client;

public class ClientService : IProcessable, IClientService, IMessageReceiver
{
    private DateTime _tokenExpiry;
    private bool _recreateToken = false;
    
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly Queue<IRequestTask> _pendingRequests = new Queue<IRequestTask>();
    private readonly RequestTaskResults _taskResults = new RequestTaskResults();
    
    public IProcessable Processable => this;
    public IRequestTaskResultProvider ResultProvider => _taskResults;
    public bool IsFinished { get; private set; }

    public void Init()
    {
        Game.MessageManager.AddMessageReceiver(this);
    }
    
    public void Process(float deltaTime)
    {
        if (_recreateToken && DateTime.Now > _tokenExpiry)
        {
            _pendingRequests.Enqueue(new RequestTaskRecreateToken());
            _recreateToken = false;
        }
        if (_pendingRequests.Count == 0)
        {
            IsFinished = true;
            return;
        }
        if (_pendingRequests.Peek().IsFailed)
        {
            _pendingRequests.Dequeue().OnFailureMessaging(Game.MessageManager);
        }
        else if (_pendingRequests.Peek().IsCompleted)
        {
            _pendingRequests.Peek().OnSuccessMessaging(Game.MessageManager);
            _pendingRequests.Dequeue().ApplyResults(_taskResults);
        }
        else if (!_pendingRequests.Peek().IsStarted)
        {
            _pendingRequests.Peek().SendRequest(_httpClient);
        }
        IsFinished = false;
    }

    public void AddRequest(RequestType type, UserNamePassword userNamePassword)
    {
        switch (type)
        {
            case RequestType.CreateUser:
            {
                _pendingRequests.Enqueue(new RequestTaskCreateUser(userNamePassword));
                break;
            }
            case RequestType.Login:
            {
                _pendingRequests.Enqueue(new RequestTaskLogin(userNamePassword));
                break;
            }
        }
    }
    public void AddRequest(RequestType type)
    {
        switch (type)
        {
            case RequestType.CreateUser:
            {
                break;    
            }
            case RequestType.RecreateToken:
            {
                break;
            }
            case RequestType.GetUsers:
            {
                _pendingRequests.Enqueue(new RequestTaskUsers());
                break;
            }
        }
    }

    public void Logout()
    {
        Game.Main.Log("Client: Logout");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "i_am_TOKEN_to_you");
        _recreateToken = false;
    }

    public void Message(MessageType type)
    {
        switch (type)
        {
            case MessageType.NewTokenAvailable:
                TokenTime tt = ResultProvider.ObtainTokenTime();
                if (tt != null)
                {
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tt.Token);
                    _tokenExpiry = DateTime.Now.AddMilliseconds(tt.Time/4*3);
                    _recreateToken = true;
                }
                break;
        }
    }
}