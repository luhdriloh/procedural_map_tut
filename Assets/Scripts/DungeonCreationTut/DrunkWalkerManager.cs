using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    NORTH = 0,
    EAST = 1,
    SOUTH = 2,
    WEST = 3
};

public class DrunkWalkerManager : MonoBehaviour
{
    private static readonly Dictionary<Direction, Vector2Int> _directionMovementMapping = new Dictionary<Direction, Vector2Int>
    {
        { Direction.NORTH, Vector2Int.up },
        { Direction.SOUTH, Vector2Int.down },
        { Direction.WEST, Vector2Int.left },
        { Direction.EAST, Vector2Int.right }
    };

    public static HashSet<Vector2Int> CreateMap(LevelCreationData levelData)
    {
        List<DrunkWalker> drunkWalkers = new List<DrunkWalker>();
        HashSet<Vector2Int> positionsVisited = new HashSet<Vector2Int>();

        for (int i = 0; i < levelData._numberOfWalkers; i++)
        {
            drunkWalkers.Add(new DrunkWalker(Vector2Int.zero));
        }

        for (int i = 0; i < levelData._numberOfIterations; i++)
        {
            foreach (DrunkWalker drunkWalker in drunkWalkers)
            {
                Vector2Int newPosition = drunkWalker.Move(_directionMovementMapping);
                positionsVisited.Add(newPosition);
            }
        }

        return positionsVisited;
    }
}
