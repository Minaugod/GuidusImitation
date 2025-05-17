using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Wall : MonoBehaviour
{
    private Vector3Int wallPos;
    private void OnEnable()
    {
        wallPos = Vector3Int.FloorToInt(transform.position);
        TileLockManager.Instance.LockTile(wallPos);
    }
}
