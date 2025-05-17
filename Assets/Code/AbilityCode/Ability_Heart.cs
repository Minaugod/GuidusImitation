using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Heart : Ability
{

    public override void Activate()
    {
        // 최대체력 + 2
        allowCount++;
        GameManager.Instance.playerMaxHp += 2;
        GameManager.Instance.UpdateHp(2);

    }

    public override void Deactivate()
    {

        for(int i = 0; i < allowCount; i++)
        {
            GameManager.Instance.playerMaxHp -= 2;
        }
        

    }

    private void Awake()
    {
        abilityName = "빨간 하트";
        abilityDesc = "최대체력이 증가합니다.";
    }

}
