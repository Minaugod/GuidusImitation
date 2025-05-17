using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDot : MonoBehaviour
{
    EnemyController target;
    public int damage;
    private void OnEnable()
    {
        target = GetComponentInParent<EnemyController>();

        InvokeRepeating("DamageIt", 1f, 1f);
        Invoke("DestroyIt", 3f);
    }

    private void DamageIt()
    {
        target.Hp -= damage;
    }

    private void DestroyIt()
    {
        Destroy(gameObject);
    }

}
