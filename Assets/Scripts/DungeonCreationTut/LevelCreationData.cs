using UnityEngine;

[CreateAssetMenu(fileName = "LevelCreationData.asset", menuName = "LevelCreationData/Level Data")]
public class LevelCreationData : ScriptableObject
{
    public int _numberOfWalkers;
    public int _numberOfIterations;

    public int _tileSize;
}
