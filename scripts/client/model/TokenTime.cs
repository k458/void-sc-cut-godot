using System.Runtime.InteropServices.JavaScript;

namespace voidsccut.scripts.client.model;

public class TokenTime
{
    public string Token{get; set;}
    public int Time{get; set;}

    public override string ToString()
    {
        return "Token: "+Token+" Time: "+Time;
    }
}