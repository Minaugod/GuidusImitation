using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileLockManager : MonoBehaviour
{


    public HashSet<Vector3Int> lockedTiles = new HashSet<Vector3Int>();

    public bool IsTileLocked(Vector3Int tilePosition)
    {
        return lockedTiles.Contains(tilePosition);
    }

    public void LockTile(Vector3Int tilePosition)
    {
        lockedTiles.Add(tilePosition);
    }

    public void UnlockTile(Vector3Int tilePosition)
    {
        lockedTiles.Remove(tilePosition);
    }

    public void ResetLock()
    {
        lockedTiles = new HashSet<Vector3Int>();
    }


    private static TileLockManager instance;
    public static TileLockManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
