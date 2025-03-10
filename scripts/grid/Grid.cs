using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using voidsccut.scripts.shared;

namespace voidsccut.scripts.grid;

public class Grid : IGrid
{
    private readonly GridCalculator _gridCalculator;
    public bool IsCalculating => _isCalculating == 1;
    private int _isCalculating = 0;
    
    public IReadOnlyList<ITile> AllyTiles => _gridCalculator.AllyTiles;
    public IReadOnlyList<ITile> EnemyTiles => _gridCalculator.EnemyTiles;

    // calc
    private IEntity _entityCalcFor;
    private int _movementLeftCalcFor;
    private ITile _tileCalcFor;
    
    //grid
    private readonly Tile[,] _tiles;

    public Grid(int size)
    {
        _tiles = new Tile[size, size];
        for (int x = 0; x < _tiles.GetLength(0); x++)
        {
            for (int y = 0; y < _tiles.GetLength(1); y++)
            {
                _tiles[x, y] = new Tile(x, y);
            }
        }
        _gridCalculator = new GridCalculator(_tiles);
        _gridCalculator.AssignNeighbors();
    }
    
    public bool GetIsCalculatedFor(IEntity entity)
    {
        return !IsCalculating && 
               entity == _entityCalcFor && 
               _tileCalcFor == entity.Tile &&
               _movementLeftCalcFor == entity.MovementLeft;
    }

    public ITile GetTile(int x, int y)
    {
        if (x < 0 || x >= _tiles.GetLength(0)) return null;
        if (y < 0 || y >= _tiles.GetLength(1)) return null;
        return _tiles[x, y];
    }

    public void GetPath(ITile tile, List<ITile> path) => _gridCalculator.GetPath(tile, path);
    
    public void PairTileAndEntity(ITile tile, IEntity entity)
    {
        Tile pairTile = _tiles[tile.X, tile.Y];
        if (pairTile.Entity != null)
        {
            Game.Main.Log("ERROR: Tried to pair "+entity.Name+" with tile "+pairTile.X+":"+pairTile.Y+", that is already paired with "+pairTile.Entity.Name);
            return;
        }
        if (entity != null)
        {
            if (entity.Tile != null)
            {
                Tile oldTile = _tiles[entity.Tile.X, entity.Tile.Y];
                if (oldTile.Entity == entity)
                {
                    oldTile.SetEntity(null);
                }
            }
            pairTile.SetEntity(entity);
            entity.Manipulator.SetTile(pairTile);
        }
        else
        {
            pairTile.SetEntity(null);
        }
    }

    public int GetPathCost(ITile from, ITile to)
    {
        if (from.X == to.X || from.Y == to.Y) return 2;
        return 3;
    }
    
    public async Task CalculateForEntityAsync(IEntity entity)
    {
        if (Interlocked.Exchange(ref _isCalculating, 1) == 1) return;
        if (entity == null || entity.Tile == null) return;
        if (GetIsCalculatedFor(entity))
        {
            _isCalculating = 0;
            return;
        }
        _entityCalcFor = entity;
        _tileCalcFor = entity.Tile;
        _movementLeftCalcFor = entity.MovementLeft;
        try
        {
            await Task.Run(() => _gridCalculator.Cascade(entity));
        }
        finally
        {
            _isCalculating = 0;
        }
    }
}