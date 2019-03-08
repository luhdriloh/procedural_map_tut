using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrunkWalker
{
    public Vector2Int Position { get; set; }

    public DrunkWalker(Vector2Int startPosition)
    {
        Position = startPosition;
    }

    public Vector2Int Move(Dictionary<Direction, Vector2Int> _directionMovementMapping)
    {
        Direction toMove = (Direction)Random.Range(0, _directionMovementMapping.Count);
        Position += _directionMovementMapping[toMove];
        return Position;
    }
}
