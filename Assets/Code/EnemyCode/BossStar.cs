using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class BossStar : EnemyController
{
    [SerializeField]
    private GameObject trianglePrefab;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private GameObject coinPrefab;

    protected override void Awake()
    {
        base.Awake();
        attackAnimationName = "BossAttack";

    }

    protected override void Attacked(Vector3Int pos)
    {

        Vector3 spawnPos = Vector3.zero;
        Vector3Int currentCell = Tilemap.WorldToCell(transform.position);
        int randomDirection = Random.Range(0, 4);
        Vector3Int movemDirection = Vector3Int.zero;
        switch (randomDirection)
        {
            case 0:
                movemDirection = Vector3Int.up;
                break;
            case 1:
                movemDirection = Vector3Int.right;
                break;
            case 2:
                movemDirection = Vector3Int.down;
                break;
            case 3:
                movemDirection = Vector3Int.left;
                break;
        }
        TileBase spawnTile = Tilemap.GetTile(currentCell + movemDirection);


        if (!TileLockManager.Instance.IsTileLocked(currentCell + movemDirection) && spawnTile != null)
        {
            spawnPos = Tilemap.GetCellCenterWorld(currentCell + movemDirection);
            GameObject triangle = Instantiate(trianglePrefab, spawnPos, Quaternion.identity, Tilemap.transform);
        }

        else
        {
            Vector3 playerPos = Tilemap.GetCellCenterWorld(pos);
            Vector2 direction = playerPos - transform.position;

            direction.Normalize();

            FireProjectile(direction);
        }

    }

    protected override IEnumerator Dead()
    {

        Vector3 spawnItem = transform.position;
        spawnItem.y += 0.3f;
        // coin µå¶ø
        GameObject coin = Instantiate(coinPrefab, spawnItem, Quaternion.identity, Tilemap.transform);
        Coin coinValue = coin.GetComponent<Coin>();
        coinValue.Amount = Random.Range(20, 31);
        return base.Dead();
    }

    private void FireProjectile(Vector2 direction)
    {

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, Tilemap.transform);


        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

        rb.velocity = direction * projectileSpeed;

    }


}
