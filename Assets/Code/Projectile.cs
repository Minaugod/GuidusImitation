using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage;
    private void OnEnable()
    {
        Invoke("DestoryProjectile", 1.5f);
    }

    private void DestoryProjectile()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other != null)
        {
            if (other.CompareTag("Player"))
            {
                DestoryProjectile();
                GameManager.Instance.PlayerHit(damage);

            }
        }
        
    }


}
