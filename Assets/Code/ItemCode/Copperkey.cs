using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Copperkey : Item
{
    protected override void GetItem()
    {
        GameManager.Instance.playerCopperkey += 1;

        base.GetItem();
    }
}
