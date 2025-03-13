using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Godot;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskLogin(UserNamePassword userNamePassword) : RequestTask
{
    private TokenTime _tokenTime;
    private string _log;
    
    protected override void OnStart()
    {
        Task task = Do("/login");
    }
    protected async Task Do(string url)
    {
        HttpResponseMessage response;
        //response = await Client.PostAsJsonAsync(url, userNamePassword);
        try
        {
            GD.Print("Response from "+Client.BaseAddress+url);
            response = await Client.PostAsJsonAsync(url, userNamePassword);
        }
        catch (Exception e)
        {
            _log = "Authorization failed: cannot connect to server.";
            IsFailed = true;
            return;
        }
        
        if (!response.IsSuccessStatusCode)
        {
            _log = "Authorization failed: forbidden or bad request.";
            IsFailed = true;
            return;
        }
        
        try
        {
            _tokenTime = await response.Content.ReadFromJsonAsync<TokenTime>(Config.JsonOptions);
        }
        catch (Exception e)
        {
            _log = "Authorization failed: cannot parse token.";
            IsFailed = true;
            return;
        }
        IsFinished = true;
    }
    
    protected override void OnFailure()
    {
        MessageTransmitter.TransmitMessage(MessageType.ClientAuthorizationFailed, _log);
    }

    protected override void OnFinish()
    {
        Aggregator.SetTokenTime(_tokenTime);
        MessageTransmitter.TransmitMessage(MessageType.NewTokenAvailable);
        MessageTransmitter.TransmitMessage(MessageType.ClientAuthorized);
    }
}