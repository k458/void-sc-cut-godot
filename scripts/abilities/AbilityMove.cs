using System.Collections.Generic;
using voidsccut.scripts.shared;

namespace voidsccut.scripts.abilities;

public class AbilityMove : Ability
{
    private readonly List<ITile> _path = new List<ITile>();
    private int _nextTileIndex;
    
    
    public AbilityMove() : base
    (
        "Move",
        "Move to tile, if possible",
        "ability.move",
        0,
        0,
        0
    )
    {
    }

    
    protected override void OnProcess()
    {
        ITile nextTile = _path[_nextTileIndex];
        bool reached = Entity.Manipulator.TranslateTowards(nextTile, DeltaTime * 1f);
        if (reached)
        {
            Game.Grid.PairTileAndEntity(nextTile, Entity);
            _nextTileIndex--;
            if (_nextTileIndex >= 0)
            {
                nextTile = _path[_nextTileIndex];
                int pathCost = Game.Grid.GetPathCost(Entity.Tile, nextTile);
                Entity.Manipulator.SetMovementLeft(Entity.MovementLeft - pathCost);
            }
            else
            {
                IsFinished = true; 
            }
        }
    }

    protected override void OnUse()
    {
        Game.Grid.GetPath(Tile, _path);
        if (_path.Count == 0)
        {
            IsFinished = true;
        }
        else
        {
            _nextTileIndex = _path.Count - 2;
            ITile nextTile = _path[_nextTileIndex];
            int pathCost = Game.Grid.GetPathCost(Entity.Tile, nextTile);
            Entity.Manipulator.SetMovementLeft(Entity.MovementLeft - pathCost);
        }
    }

    protected override void OnBeforeReset()
    {
        _path.Clear();
    }
}