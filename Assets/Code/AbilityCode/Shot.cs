using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private EnemyController ignoreEnemy;

    public EnemyController IgnoreEnemy
    {
        get
        {
            return ignoreEnemy;
        }
        set
        {
            this.ignoreEnemy = value;
        }
    }
    private void OnEnable()
    {
        Invoke("DestoryProjectile", 0.5f);
    }

    private void DestoryProjectile()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if(enemy != null && enemy != ignoreEnemy)
            {
                DestoryProjectile();
                enemy.Hp -= damage;
            }

        }

    }
}
