using System.Collections.Generic;
using voidsccut.scripts.shared;

namespace voidsccut.scripts.grid;

internal class GridCalculator
{
    // out
    public IReadOnlyList<ITile> AllyTiles => _allyTiles.AsReadOnly();
    public IReadOnlyList<ITile> EnemyTiles => _enemyTiles.AsReadOnly();
    
    
    private static readonly int PathCost = 2;
    private static readonly int PathCostD = 3;
    
    private readonly Tile[,] _tiles;
    
    //calc
    private List<Tile> _tilesOne = new List<Tile>(100);
    private List<Tile> _tilesTwo = new List<Tile>(100);
    private List<Tile> _tilesThree = new List<Tile>(100);
    private List<Tile> _tilesReset = new List<Tile>(100);
    
    private readonly List<ITile> _allyTiles = new List<ITile>(10);
    private readonly List<ITile> _enemyTiles = new List<ITile>(10);

    public GridCalculator(Tile[,] tiles)
    {
        _tiles = tiles;
    }
    public void Cascade(IEntity entity)
    {
        foreach (var tileReset in _tilesReset)
        {
            tileReset.ProceduralData.Reset();
        }
        _tilesReset.Clear();
        _tilesOne.Clear();
        _tilesThree.Clear();
        _allyTiles.Clear();
        _enemyTiles.Clear();
        
        Tile startTile = _tiles[entity.Tile.X, entity.Tile.Y];
        _tilesReset.Add(startTile);
        startTile.ProceduralData.SetMovementLeft(entity.MovementLeft);
        startTile.ProceduralData.SetReached(true);
        if (entity.MovementLeft == PathCost)
        {
            TileProceduralData pd = startTile.ProceduralData;
            int movementLeftNew = 0;
            if (pd.Neighbors[1] != null) CascadeProcessNext(pd.Neighbors[1], startTile, movementLeftNew, _tilesOne);
            if (pd.Neighbors[3] != null) CascadeProcessNext(pd.Neighbors[3], startTile, movementLeftNew, _tilesOne);
            if (pd.Neighbors[4] != null) CascadeProcessNext(pd.Neighbors[4], startTile, movementLeftNew, _tilesOne);
            if (pd.Neighbors[6] != null) CascadeProcessNext(pd.Neighbors[6], startTile, movementLeftNew, _tilesOne);
        }
        else
        {
            _tilesOne.Add(startTile);
        }
        while (_tilesOne.Count > 0)
        {
            foreach (var tileCur in _tilesOne)
            {
                _tilesReset.Add(tileCur);
                if (tileCur.Entity != null)
                {
                    if (tileCur.Entity.Team == entity.Team) _allyTiles.Add(tileCur);
                    else _enemyTiles.Add(tileCur);
                }
                else
                {
                    TileProceduralData pd = tileCur.ProceduralData;
                    int movementLeftNew = pd.MovementLeft - PathCost;
                    int movementLeftNewD = pd.MovementLeft - PathCostD;
                    if (pd.Neighbors[1] != null) CascadeProcessNext(pd.Neighbors[1], tileCur, movementLeftNew, _tilesTwo);
                    if (pd.Neighbors[3] != null) CascadeProcessNext(pd.Neighbors[3], tileCur, movementLeftNew, _tilesTwo);
                    if (pd.Neighbors[4] != null) CascadeProcessNext(pd.Neighbors[4], tileCur, movementLeftNew, _tilesTwo);
                    if (pd.Neighbors[6] != null) CascadeProcessNext(pd.Neighbors[6], tileCur, movementLeftNew, _tilesTwo);
                    if (pd.Neighbors[0] != null) CascadeProcessNext(pd.Neighbors[0], tileCur, movementLeftNewD, _tilesTwo);
                    if (pd.Neighbors[2] != null) CascadeProcessNext(pd.Neighbors[2], tileCur, movementLeftNewD, _tilesTwo);
                    if (pd.Neighbors[5] != null) CascadeProcessNext(pd.Neighbors[5], tileCur, movementLeftNewD, _tilesTwo);
                    if (pd.Neighbors[7] != null) CascadeProcessNext(pd.Neighbors[7], tileCur, movementLeftNewD, _tilesTwo);
                }
                
            }
            _tilesOne.Clear();
            (_tilesOne, _tilesTwo) = (_tilesTwo, _tilesOne);
        }
    }
    private void CascadeProcessNext(Tile tile, Tile linkedBack, int movementLeftNew, List<Tile> tileList)
    {
        if (tile.ProceduralData.MovementLeft < movementLeftNew || !tile.ProceduralData.Reached)
        {
            tile.ProceduralData.SetMovementLeft(movementLeftNew);
            tile.ProceduralData.SetLinkedBack(linkedBack);
            tile.ProceduralData.SetReached(true);
            tileList.Add(tile);
        }
    }

    public void GetPath(ITile tile, List<ITile> path)
    {
        path.Clear();
        Tile t = _tiles[tile.X, tile.Y];
        if (t.ProceduralData.Reached)
        {
            path.Add(t);
            while (t.ProceduralData.LinkedBack != null)
            {
                t = t.ProceduralData.LinkedBack;
                path.Add(t);
            }
        }
    }
    
    public void AssignNeighbors()
    {
        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                Tile tile = _tiles[x, y];
                if (x > 0)
                {
                    if (y > 0) tile.ProceduralData.SetNeighbor(_tiles[x - 1, y - 1], 0);
                    tile.ProceduralData.SetNeighbor(_tiles[x - 1, y], 3);
                    if (y < _tiles.GetLength(1) - 1) tile.ProceduralData.SetNeighbor(_tiles[x - 1, y + 1], 5);
                }
                if (x < _tiles.GetLength(0) - 1)
                {
                    if (y > 0) tile.ProceduralData.SetNeighbor(_tiles[x + 1, y - 1], 2);
                    tile.ProceduralData.SetNeighbor(_tiles[x + 1, y], 4);
                    if (y < _tiles.GetLength(1) - 1) tile.ProceduralData.SetNeighbor(_tiles[x + 1, y + 1], 7);
                }
                if (y > 0) tile.ProceduralData.SetNeighbor(_tiles[x, y - 1], 1);
                if (y < _tiles.GetLength(1) - 1) tile.ProceduralData.SetNeighbor(_tiles[x, y + 1], 6);
            }
        }
    }
}