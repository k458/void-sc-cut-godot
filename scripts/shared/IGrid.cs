using System.Collections.Generic;
using System.Threading.Tasks;

namespace voidsccut.scripts.shared;

public interface IGrid
{
    public bool IsCalculating { get; }
    public bool GetIsCalculatedFor(IEntity entity);
    public ITile GetTile(int x, int y);
    public void PairTileAndEntity(ITile tile, IEntity entity);
    public int GetPathCost(ITile from, ITile to);
    public Task CalculateForEntityAsync(IEntity entity);
    public void GetPath(ITile tile, List<ITile> path);
    public IReadOnlyList<ITile> AllyTiles { get; }
    public IReadOnlyList<ITile> EnemyTiles { get; }
}