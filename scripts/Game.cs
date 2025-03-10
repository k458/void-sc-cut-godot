using voidsccut.scripts.client;
using voidsccut.scripts.grid;
using voidsccut.scripts.main;
using voidsccut.scripts.shared;

namespace voidsccut.scripts;

public static class Game
{
    public static IMain Main { get; private set; }
    public static IClientService ClientService { get; } = new ClientService();
    public static IPlayer Player { get; } = new Player();
    public static IGrid Grid { get; } = new Grid(100);

    public static GameState State { get; private set; } = GameState.None;
    
    public static void SetMain( Main main )
    {
        if (Main != null) Main.Log("ERROR: Cannot assign Main - Main is already assigned!");
        else Main = main;
    }
    
    public static void SetState(GameState gameState) => State = gameState;
}