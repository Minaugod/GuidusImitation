using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]
    private Animator spinAnim;

    private BoxCollider2D _collider;

    private void Start()
    {

        spinAnim = GetComponent<Animator>();
        _collider = GetComponent<BoxCollider2D>();
        _collider.enabled = false;
        StartCoroutine(SpinAndFall());
       
    }

    private void Update()
    {
        if (spinAnim.GetCurrentAnimatorStateInfo(0).IsName("SpinAnim") && spinAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            spinAnim.SetBool("Spin", true);

        }
    }
    IEnumerator SpinAndFall()
    {

        spinAnim.SetBool("Spin", false);
        Vector3 currentPos = transform.position;
        Vector3 fall = transform.position;
        fall.y = fall.y - 0.3f;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / 0.2f;
            transform.position = Vector3.Lerp(currentPos, fall, t);

            yield return null;
        }
        _collider.enabled = true;

        yield return null;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null)
        {
            
            if (other.CompareTag("Player"))
            {

                GetItem();
                return;
            }
            
        }


    }

    protected virtual void GetItem()
    {
        Destroy(gameObject);
    }



}