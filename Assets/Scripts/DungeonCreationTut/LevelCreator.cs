using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The purpose of this class is SOLELY to create the level layout.
/// </summary>
public class LevelCreator : MonoBehaviour
{
    public LevelCreationData _levelCreationData;
    public Tilemap _tilemapToDrawFloor;
    public TileBase _fillRuleTile;

    private HashSet<Vector2Int> _dungeonTiles;

    private void Start()
    {
        _dungeonTiles = DrunkWalkerManager.CreateMap(_levelCreationData);
        if (_levelCreationData._tileSize > 1)
        {
            _dungeonTiles = ReturnListOfScaledTiles(_dungeonTiles);
        }

        DrawDungeonTiles(_dungeonTiles, _tilemapToDrawFloor, _fillRuleTile);
    }


    private void DrawDungeonTiles(IEnumerable<Vector2Int> tiles, Tilemap tilemapToUse, TileBase tilesToUse)
    {
        foreach (Vector2Int tileLocation in tiles)
        {
            tilemapToUse.SetTile(new Vector3Int(tileLocation.x, tileLocation.y, 0), tilesToUse);
        }
    }


    private HashSet<Vector2Int> ReturnListOfScaledTiles(IEnumerable<Vector2Int> tiles)
    {
        HashSet<Vector2Int> scaledTiles = new HashSet<Vector2Int>();

        foreach (Vector2Int tileLocation in tiles)
        {
            Vector2Int startPosition = tileLocation * _levelCreationData._tileSize;
            Vector2Int newPosition;

            for (int i = 0; i < _levelCreationData._tileSize; i++)
            {
                for (int j = 0; j < _levelCreationData._tileSize; j++)
                {
                    newPosition = new Vector2Int(i, j) + new Vector2Int(startPosition.x, startPosition.y);
                    scaledTiles.Add(newPosition);
                }
            }
        }

        return scaledTiles;
    }
}



























