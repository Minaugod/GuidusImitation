using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    private int amount;
    
    public int Amount
    {
        get
        {
            return amount;
        }

        set
        {
            this.amount = value;
        }
    }
    protected override void GetItem()
    {
        GameManager.Instance.playerCoin += amount;
        ScoreManager.Instance.earnCoinCount += amount;
        base.GetItem();
    }

}
