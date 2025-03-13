using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared.serverTypes;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskCreateUser(UserNamePassword userNamePassword) : RequestTaskLogin(userNamePassword)
{
    protected override void OnStart()
    {
        Task task = Do("/createUser");
    }
}