using System;

namespace voidsccut.scripts.shared.serverTypes;

public class UserEntity
{
    public long Id{ get; set; }
    public string Name{ get; set; }
    public string Password{ get; set; }
    public string Role{ get; set; }
}