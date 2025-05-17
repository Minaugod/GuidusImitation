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
        abilityName = "ä�� No.5";
        abilityDesc = "�÷��̾ ���ظ� ������, ���� Ȯ���� �ǵ尡 �����Ѵ�.";
    }
}
