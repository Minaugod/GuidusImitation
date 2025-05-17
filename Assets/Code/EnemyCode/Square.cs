using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : EnemyController
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float projectileSpeed = 10f;

    [SerializeField]
    private GameObject copperkeyPrefab;
    protected override void Awake()
    {
        base.Awake();
        attackAnimationName = "Enemy1AtkAnim";

    }


    protected override void Attacked(Vector3Int pos)
    {

        Vector3 playerPos = Tilemap.GetCellCenterWorld(pos);
        Vector2 direction = playerPos - transform.position;


        direction.Normalize();

        FireProjectile(direction);

    }

    protected override IEnumerator Dead()
    {

        int reward = Random.Range(1, 11);
        Vector3 spawnItem = transform.position;
        spawnItem.y = spawnItem.y + 0.3f;
        if (reward > 5)
        {
            // copperkey µå¶ø
            GameObject copperkey = Instantiate(copperkeyPrefab, spawnItem, Quaternion.identity, Tilemap.transform);

        }

        return base.Dead();
    }

    private void FireProjectile(Vector2 direction)
    {

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity, Tilemap.transform);


        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        
        rb.velocity = direction * projectileSpeed;

    }

}
