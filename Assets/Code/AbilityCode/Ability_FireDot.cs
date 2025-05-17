using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_FireDot : Ability
{
    [SerializeField]
    private GameObject fireDotPrefab;

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
        if(allow && GameManager.Instance.player.Attacking && ready)
        {
            
            EnemyController enemy = GameManager.Instance.player.AtkTarget;
            if(enemy != null)
            {
                ready = false;
                FireDot(enemy);
                  
            }
            
            
        }

        if(allow && !GameManager.Instance.player.Attacking && !ready)
        {
            ready = true;
        }
    }

    private void FireDot(EnemyController enemy)
    {
        int chance = Random.Range(1, 11);
        FireDot fireDotCheck = enemy.GetComponentInChildren<FireDot>();
        
        if (fireDotCheck == null && chance > 4)
        {
            GameObject fireDot = Instantiate(fireDotPrefab, enemy.transform.position, Quaternion.identity, enemy.transform);

        }


    }

    private void Awake()
    {
        abilityName = "점화";
        abilityDesc = "공격시 일정확률로 불을 붙인다.";
    }

}
