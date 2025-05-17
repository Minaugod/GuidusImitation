using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class EnemyController : MonoBehaviour
{
    private Transform playerTransform;
    
    private Tilemap tilemap;
    public Tilemap Tilemap
    {
        get
        {
            return tilemap;
        }
    }

    [SerializeField]
    private float speed;

    [SerializeField]
    private float followDistance;

    [SerializeField]
    private int hp;
    public int Hp
    {

        get
        {
            return hp;
        }
        set
        {
            this.hp = value;
            if (hp <= 0 && alive)
            {
                alive = false;
                boxCollider2D.enabled = false;
                StopCoroutine(MonsterBehavior());
                AttackEf.StopPlayback();
                StartCoroutine(Dead());

            }
        }
    }

    [SerializeField]
    private int atk;

    [SerializeField]
    private float atkRange;

    private bool attacking;

    private float distanceToPlayer;

    private Animator AttackEf;

    private Animator attackAnim;

    private bool alive = true;

    private Vector3Int nextTilePos;

    protected string attackAnimationName;

    private BoxCollider2D boxCollider2D;

    private void OnEnable()
    {
        Vector3Int currentTile = tilemap.WorldToCell(transform.position);
        Vector3 currentTileCenter = tilemap.GetCellCenterWorld(currentTile);
        transform.position = currentTileCenter;
        TileLockManager.Instance.LockTile(currentTile);
        StartCoroutine(MonsterBehavior());
    }
    protected virtual void Awake()
    {
        attackAnim = GetComponent<Animator>();
        AttackEf = GetComponentsInChildren<Animator>()[1];
        tilemap = GetComponentInParent<Tilemap>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerTransform = GameManager.Instance.player.transform;

    }
    private void Update()
    {
        if (attacking)
        {
            if (attackAnim.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationName) && attackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                attackAnim.SetBool("Attack", false);
                AttackEf.SetBool("Active", false);
                Attacked(nextTilePos);
                attacking = false;
            }

        }

    }

    protected virtual IEnumerator Dead()
    {

        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        TileLockManager.Instance.UnlockTile(currentCell);
        TileLockManager.Instance.UnlockTile(nextTilePos);
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        float fadeTime = 0.5f;

        for (float t = 0.01f; t < fadeTime; t += Time.deltaTime)
        {


            float newAlpha = Mathf.Lerp(1f, 0f, t / fadeTime);


            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, newAlpha);


            yield return null;
        }
        GameManager.Instance.nowRoomData.EnemyCount -= 1;
        Destroy(gameObject);
    }

    protected virtual void Attacked(Vector3Int pos)
    {

        Vector3 attackPos = tilemap.GetCellCenterWorld(pos);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos, 0.2f);

        foreach (Collider2D collider in colliders)
        {
            
            if (collider != null)
            {

                if (collider.CompareTag("Player"))
                {
                    GameManager.Instance.PlayerHit(atk);
                    
                    break;
                }

            }
        }
    }

    IEnumerator MonsterBehavior()
    {
        yield return new WaitForSeconds(speed);

        while (alive)
        {

            bool cantMove = true;
            float t = 0f;
            Vector3 currentPos = transform.position;
            Vector3Int currentCell = tilemap.WorldToCell(transform.position);
            distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);
            if (distanceToPlayer <= followDistance)
            {


                Vector3Int playerCell = tilemap.WorldToCell(playerTransform.position);

                Vector3Int moveDirection = Vector3Int.zero;
                if (Mathf.Abs(currentCell.x - playerCell.x) > Mathf.Abs(currentCell.y - playerCell.y))
                {
                    moveDirection = new Vector3Int(Mathf.RoundToInt(Mathf.Sign(playerCell.x - currentCell.x)), 0, 0);
                }
                else
                {
                    moveDirection = new Vector3Int(0, Mathf.RoundToInt(Mathf.Sign(playerCell.y - currentCell.y)), 0);
                }


                nextTilePos = currentCell + new Vector3Int(moveDirection.x, moveDirection.y, 0);
                TileBase nextTile = tilemap.GetTile(nextTilePos);
                Vector3 nextPos = tilemap.GetCellCenterWorld(nextTilePos);

                RaycastHit2D[] ray = Physics2D.RaycastAll(transform.position, nextPos - transform.position, atkRange);

                for (int i = 0; i < ray.Length; i++)
                {
                    RaycastHit2D Hit = ray[i];

                    if (Hit.collider != null)
                    {
                        if (Hit.collider.CompareTag("Player"))
                        {

                            AttackEf.SetBool("Active", true);

                            yield return new WaitForSeconds(0.5f);

                            attackAnim.SetBool("Attack", true);
                            attacking = true;
                            cantMove = false;
                            break;
                        }

                    }
                }


                if (nextTile != null && cantMove)
                {
                    cantMove = false;

                    if (!TileLockManager.Instance.IsTileLocked(nextTilePos))
                    {
                        TileLockManager.Instance.LockTile(nextTilePos);
                        while (t < 1)
                        {
                            
                            t += Time.deltaTime / 0.2f;
                            transform.position = Vector3.Lerp(currentPos, tilemap.GetCellCenterWorld(nextTilePos), t);
                            
                            yield return null;
                        }
                        TileLockManager.Instance.UnlockTile(currentCell);

                    }

                    else
                    {
                        cantMove = true;
                    }

                }

            }


            // ·£´ý ÀÌµ¿
            if(cantMove)
            {

                int randomDirection;
                bool notBlocked = false;
                while (!notBlocked)
                {
                    bool find = false;
                    randomDirection = Random.Range(0, 4);
                    Vector3Int movementDirection = Vector3Int.zero;
                    switch (randomDirection)
                    {
                        case 0:
                            movementDirection = Vector3Int.up;
                            break;
                        case 1:
                            movementDirection = Vector3Int.right;
                            break;
                        case 2:
                            movementDirection = Vector3Int.down;
                            break;
                        case 3:
                            movementDirection = Vector3Int.left;
                            break;
                    }

                    nextTilePos = currentCell + new Vector3Int(movementDirection.x, movementDirection.y, 0);
                    TileBase nextTile = tilemap.GetTile(nextTilePos);

                    if (nextTile != null && !find)
                    {
                        
                        t = 0;
                        notBlocked = true;

                        if (!TileLockManager.Instance.IsTileLocked(nextTilePos))
                        {
                            TileLockManager.Instance.LockTile(nextTilePos);
                            while (t < 1)
                            {
                                
                                t += Time.deltaTime / 0.2f;
                                transform.position = Vector3.Lerp(currentPos, tilemap.GetCellCenterWorld(nextTilePos), t);
                                
                                yield return null;
                            }
                            TileLockManager.Instance.UnlockTile(currentCell);
                        }

                        else
                        {
                            notBlocked = false;
                        }

                    }
                    yield return null;
                }

            }
            yield return new WaitForSeconds(speed);

        }

    }

}
