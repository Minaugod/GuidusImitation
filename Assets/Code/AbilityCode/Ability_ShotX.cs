using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_ShotX : Ability
{
    [SerializeField]
    private GameObject shotPrefab;

    private bool ready = true;
    public override void Activate()
    {
        allow = true;
    }

    public override void Deactivate()
    {
        allow = false;
    }

    private void Update()
    {
        if (allow && GameManager.Instance.player.Attacking && ready)
        {

            EnemyController enemy = GameManager.Instance.player.AtkTarget;
            if (enemy != null)
            {
                ready = false;
                shotPlus(enemy);

            }


        }

        if (allow && !GameManager.Instance.player.Attacking && !ready)
        {
            ready = true;
        }
    }

    private void shotPlus(EnemyController enemy)
    {
        float projectileSpeed = 7f;
        Rigidbody2D rb;
        for (int i = 0; i < 4; i++)
        {
            Shot shot = Instantiate(shotPrefab, enemy.Tilemap.transform).GetComponent<Shot>();
            shot.transform.position = enemy.transform.position;
            shot.IgnoreEnemy = enemy;
            rb = shot.GetComponent<Rigidbody2D>();
            switch (i)
            {
                case 0:
                    rb.velocity = (Vector2.up + Vector2.left) * projectileSpeed;
                    break;
                case 1:
                    rb.velocity = (Vector2.up + Vector2.right) * projectileSpeed;
                    break;
                case 2:
                    rb.velocity = (Vector2.down + Vector2.left) * projectileSpeed;
                    break;
                case 3:
                    rb.velocity = (Vector2.down + Vector2.right) * projectileSpeed;
                    break;
            }
        }




    }

    private void Awake()
    {
        abilityName = "산탄X";
        abilityDesc = "공격한 적으로부터 투사체가 X자 모양으로 발사된다.";
    }
}
