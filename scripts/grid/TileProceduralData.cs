using System.Collections.Generic;

namespace voidsccut.scripts.grid;

internal class TileProceduralData
{
    public int MovementLeft { get; private set; } = -1;
    public Tile LinkedBack { get; private set; } = null;
    public bool Reached { get; private set; } = false;
    
    public IReadOnlyList<Tile> Neighbors => _neighbors;
    private readonly Tile[] _neighbors = new Tile[8];
    
    public void SetNeighbor(Tile neighbor, int index) => _neighbors[index] = neighbor;
    public void SetMovementLeft(int value) => MovementLeft = value;
    public void SetLinkedBack(Tile tile) => LinkedBack = tile;
    public void SetReached(bool reached) => Reached = reached;
    public void Reset()
    {
        MovementLeft = -1;
        LinkedBack = null;
        Reached = false;
    }
}