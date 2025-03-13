using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using voidsccut.scripts.messageService;
using voidsccut.scripts.shared.serverTypes.progression;

namespace voidsccut.scripts.client.model.requests;

public class RequestTaskProgression : RequestTask
{
    protected override void OnStart()
    {
        throw new NotImplementedException();
    }

    protected override void OnFailure()
    {
        throw new NotImplementedException();
    }

    protected override void OnFinish()
    {
        throw new NotImplementedException();
    }
}