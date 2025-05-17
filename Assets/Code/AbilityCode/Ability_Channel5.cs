using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Channel5 : Ability
{


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
        if (allow && GameManager.Instance.player.PlayerHit && ready)
        {

            Channel5();
            ready = false;

        }

        if (allow && !GameManager.Instance.player.PlayerHit && !ready)
        {
            ready = true;
        }
    }

    private void Channel5()
    {
        int chance = Random.Range(1, 11);
        if(chance > 5)
        {
            GameManager.Instance.playerShield += 2;
        }
    }

    private void Awake()
    {
        abilityName = "채널 No.5";
        abilityDesc = "플레이어가 피해를 입을때, 일정 확률로 실드가 증가한다.";
    }
}
