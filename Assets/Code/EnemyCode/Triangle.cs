using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangle : EnemyController
{
    [SerializeField]
    private GameObject coinPrefab;
    
    protected override void Awake()
    { 
        base.Awake();
        attackAnimationName = "Enemy2AtkAnim";
    }


    protected override void Attacked(Vector3Int pos)
    {
        base.Attacked(pos);
    }


    protected override IEnumerator Dead()
    {   

        int reward = Random.Range(1, 11);
        Vector3 spawnPos = transform.position;
        spawnPos.y = spawnPos.y + 0.3f;
        if(reward > 5)
        {
            // coin µå¶ø
            Coin coin = Instantiate(coinPrefab, spawnPos, Quaternion.identity, Tilemap.transform).GetComponent<Coin>();
            coin.Amount = Random.Range(1, 11);

        }

        return base.Dead();
    }

}
