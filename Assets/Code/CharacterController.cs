using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class CharacterController : MonoBehaviour
{

    private Tilemap tilemap;
    public Tilemap Tilemap
    {
        get
        {
            return tilemap;
        }

        set
        {
            this.tilemap = value;
        }
    }

    private bool alive = true;
    private bool canMove = true;

    private bool attacking;
    public bool Attacking
    {
        get
        {
            return attacking;
        }
    }

    private bool playerHit;
    public bool PlayerHit
    {
        get
        {
            return playerHit;
        }
    }

    private EnemyController atkTarget;
    public EnemyController AtkTarget
    {
        get
        {
            return atkTarget;
        }
    }

    private Animator attackAnim;

    private SpriteRenderer spriteRenderer;

    private BoxCollider2D boxCollider2D;
    private void Awake()
    {
        attackAnim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        GameManager.Instance.player = this;
    }
    private void Update()
    {


        attackAnim.speed = GameManager.Instance.playerAtkSpd;

        if (attacking)
        {
            if (attackAnim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAtkAnim") && attackAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                attackAnim.SetBool("Attacked", true);
                canMove = true;
                attacking = false;
            }

        }

        if (GameManager.Instance.playerHp <= 0 && alive)
        {
            alive = false;
            StartCoroutine(Dead());
        }

    }

    private float ChangeRotation(Direction rotation)
    {
        float value = 0f;

        switch (rotation)
        {
            case Direction.Up:
                value = -90f;
                break;
            case Direction.Right:
                value = 180f;
                break;
            case Direction.Down:
                value = 90f;
                break;
            case Direction.Left:
                value = -0f;
                break;
        }

        return value;
    }

    public void ArrowBtnClick(Direction direction)
    {
        Vector3Int currentCell = tilemap.WorldToCell(transform.position);
        Vector3Int nextCell;
        Vector3 nextPosition;

        if (canMove && alive)
        {
            switch (direction)
            {
                case Direction.Up:
                    canMove = false;
                    transform.rotation = Quaternion.Euler(0, 0, ChangeRotation(direction));
                    nextCell = new Vector3Int(currentCell.x, currentCell.y + 1, currentCell.z);
                    nextPosition = tilemap.GetCellCenterWorld(nextCell);
                    PlayerActing(nextPosition);
                    break;

                case Direction.Down:
                    canMove = false;
                    transform.rotation = Quaternion.Euler(0, 0, ChangeRotation(direction));
                    nextCell = new Vector3Int(currentCell.x, currentCell.y - 1, currentCell.z);
                    nextPosition = tilemap.GetCellCenterWorld(nextCell);
                    PlayerActing(nextPosition);
                    break;

                case Direction.Left:
                    canMove = false;
                    transform.rotation = Quaternion.Euler(0, 0, ChangeRotation(direction));
                    nextCell = new Vector3Int(currentCell.x - 1, currentCell.y, currentCell.z);
                    nextPosition = tilemap.GetCellCenterWorld(nextCell);
                    PlayerActing(nextPosition);
                    break;

                case Direction.Right:
                    canMove = false;
                    transform.rotation = Quaternion.Euler(0, 0, ChangeRotation(direction));
                    nextCell = new Vector3Int(currentCell.x + 1, currentCell.y, currentCell.z);
                    nextPosition = tilemap.GetCellCenterWorld(nextCell);
                    PlayerActing(nextPosition);
                    break;

            }
        }
        
    }

    private void PlayerActing(Vector3 position)
    {

        Vector3Int Cell = tilemap.WorldToCell(position);
        TileBase tileCheck = tilemap.GetTile(Cell);


        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 0.27f);

        foreach (Collider2D collider in colliders)
        {


            if (collider != null)
            {
                
                if (collider.CompareTag("Enemy"))
                {

                    atkTarget = collider.GetComponent<EnemyController>();
                    Attack(atkTarget);
                    return;
                }
                if (collider.CompareTag("Wall"))
                {
                    canMove = true;
                    return;
                }

                if (collider.CompareTag("Stair"))
                {
                    
                    if (GameManager.Instance.nowRoomData != null)
                    {
                        if (GameManager.Instance.nowRoomData.upStairUnlocked == false)
                        {
                            GameManager.Instance.TryStageUp();
                            canMove = true;
                            return;
                        }
                        
                    }
                    GameManager.Instance.TryStageUp();
                    break;
                }

                if (collider.CompareTag("DownStair"))
                {
                    if (GameManager.Instance.nowRoomData != null)
                    {
                        if (GameManager.Instance.nowRoomData.downStairUnlocked == false)
                        {
                            GameManager.Instance.TryStageDown();
                            canMove = true;
                            return;
                        }

                    }
                    GameManager.Instance.TryStageDown();
                    break;
                }

                if (collider.CompareTag("SpecialStair"))
                {
                    GameManager.Instance.TryEnterSpecial();
                    break;
                }


                if (collider.CompareTag("Npc"))
                {

                    Npc npc = collider.GetComponent<Npc>();
                    npc.DisplayPopup();
                    canMove = true;
                    return;
                }

                if (collider.CompareTag("ClearStair"))
                {
                    StartCoroutine(GameManager.Instance.GameWin());
                    break;
                }
                
            }
        }


        // 다음타일이 존재한다면 이동
        if (tileCheck != null)
        {
            StartCoroutine(MoveToPosition(position));
        }


        // 다음타일이 존재하지않으면 낙사
        else if(tileCheck == null)
        {
            StartCoroutine(Falling(transform.position, position));
        }


    }
        
    private void Attack(EnemyController enemy)
    {
        attackAnim.SetBool("Attacked", false);

        enemy.Hp -= GameManager.Instance.playerAtk;
        attacking = true;

    }


    IEnumerator MoveToPosition(Vector3 position)
    {

        Vector3 currentPos = transform.position;
        Vector3Int nextTile = tilemap.WorldToCell(position);

        if (TileLockManager.Instance.IsTileLocked(nextTile))
        {
            canMove = true;
            yield break;
        }

        Vector3Int currentTile = tilemap.WorldToCell(transform.position);
        TileLockManager.Instance.LockTile(nextTile);
        float t = 0f;
        while (t < 1)
        {
            
            t += Time.deltaTime / 0.15f;
            transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;

        }
        TileLockManager.Instance.UnlockTile(currentTile);
        ScoreManager.Instance.moveTileCount++;
        canMove = true;
        yield break;
    }


    public IEnumerator Invincible()
    {
        
        playerHit = true;

        float blinkInterval = 0.1f;

        int totalBlinkCycles = 4;

        for (int i = 0; i < totalBlinkCycles; i++)
        {
            if (spriteRenderer != null)
            {
                // 보이게
                spriteRenderer.enabled = false;
                yield return new WaitForSeconds(blinkInterval);

                // 안보이게
                spriteRenderer.enabled = true;
                yield return new WaitForSeconds(blinkInterval);
            }
             
        }

        playerHit = false;
        spriteRenderer.enabled = true;


        yield return null;
    }

    IEnumerator Falling(Vector3 nowPos, Vector3 movePos)
    {
        float t = 0f;
        while (t < 1)
        {

            t += Time.deltaTime / 0.2f;
            transform.position = Vector3.Lerp(nowPos, movePos, t);
            yield return null;

        }

        spriteRenderer.sortingLayerName = "Falling";

        Vector3 fallPos = movePos;
        fallPos.y = fallPos.y - 3;

        t = 0f;
        while (t < 1)
        {

            t += Time.deltaTime / 0.5f;
            transform.position = Vector3.Lerp(movePos, fallPos, t);
            yield return null;

        }
        spriteRenderer.sortingLayerName = "Player";

        GameManager.Instance.PlayerHit(2);

        transform.position = nowPos;
        canMove = true;
        yield return null;
    }

    IEnumerator Dead()
    {
        boxCollider2D.enabled = false;
        float fadeTime = 0.7f;

        for (float t = 0.01f; t < fadeTime; t += Time.deltaTime)
        {


            float newAlpha = Mathf.Lerp(1f, 0f, t / fadeTime);

            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);


            yield return null;
        }

        ScoreManager.Instance.startAdventure = false;
        UiManager.Instance.GameOver();
        yield return null;
    }
}
