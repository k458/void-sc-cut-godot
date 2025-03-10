using voidsccut.scripts.shared;

namespace voidsccut.scripts.grid;

internal class Tile : ITile
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public IEntity Entity { get; private set; }
    public TileProceduralData ProceduralData { get; } = new TileProceduralData();

    internal Tile(int x, int y)
    {
        X = x;
        Y = y;
    }
    public void SetEntity(IEntity entity) => Entity = entity;
}