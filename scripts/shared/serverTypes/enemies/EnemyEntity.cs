namespace voidsccut.scripts.shared.serverTypes.enemies;

public class EnemyEntity
{
    public long Id {get; set;}
    public long UserId{get; set;}
    public long LocalId{get; set;}
    public string Name{get; set;}
    public string Type{get; set;}
    public int Level{get; set;}
    public int HpLeft{get; set;}
}