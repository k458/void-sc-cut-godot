using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using Godot;
using GodotPlugins.Game;
using voidsccut.scripts.client.model;
using voidsccut.scripts.client.model.requests;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared;
using voidsccut.scripts.shared.serverTypes;
using HttpClient = System.Net.Http.HttpClient;

namespace voidsccut.scripts.client;

public class ClientService : IProcessable, IClientService, IMessageReceiver
{
    private DateTime _tokenExpiry;
    private bool _recreateToken = false;

    private readonly HttpClient _httpClient = new HttpClient();
    private readonly Queue<RequestTask> _pendingRequests = new Queue<RequestTask>();
    private readonly RequestTaskResults _taskResults = new RequestTaskResults();
    
    public IProcessable Processable => this;
    public IRequestTaskResultProvider ResultProvider => _taskResults;
    public bool IsFinished { get; private set; }

    public void Init()
    {
        Game.MessageManager.AddMessageReceiver(this);
        _httpClient.Timeout = TimeSpan.FromSeconds(10);
        _httpClient.BaseAddress = new Uri("http://localhost:8090");
    }
    
    public void Process(float deltaTime)
    {
        IsFinished = true;
        if (_recreateToken && DateTime.Now > _tokenExpiry)
        {
            var task = new RequestTaskRecreateToken();
            task.Init(_httpClient, _taskResults, Game.MessageManager);
            _pendingRequests.Enqueue(task);
            _recreateToken = false;
        }
        if (_pendingRequests.Count == 0)
        {
            IsFinished = true;
            return;
        }
        else
        {
            var requestTask = _pendingRequests.Peek();
            requestTask.Process(deltaTime);
            IsFinished = requestTask.IsFinished;
            if (requestTask.IsFinished) _pendingRequests.Dequeue();
        }
    }

    public void AddRequest(RequestType type, UserNamePassword userNamePassword)
    {
        switch (type)
        {
            case RequestType.Login:
            {
                var requestTask = new RequestTaskLogin(userNamePassword);
                requestTask.Init(_httpClient, _taskResults, Game.MessageManager);
                _pendingRequests.Enqueue(requestTask);
                break;
            }
            case RequestType.CreateUser:
            { 
                var requestTask = new RequestTaskCreateUser(userNamePassword);
                requestTask.Init(_httpClient, _taskResults, Game.MessageManager);
                _pendingRequests.Enqueue(requestTask);
                break;
            }
        }
    }
    public void AddRequest(RequestType type)
    {
        switch (type)
        {
            // case RequestType.RecreateToken:
            // {
            //     break;
            // }
            // case RequestType.GetUsers:
            // {
            //     _pendingRequests.Enqueue(new RequestTaskUsers());
            //     break;
            // }
            default:
                break;
        }
    }

    public void Logout()
    {
        Game.Main.Log("Logout...");
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