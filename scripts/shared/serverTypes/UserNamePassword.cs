using System;

namespace voidsccut.scripts.shared.serverTypes;

public class UserNamePassword
{
    public string Name{get;set;}
    public string Password{get;set;}

    public override string ToString() => Name+" "+Password;
}