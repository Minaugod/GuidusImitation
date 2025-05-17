using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class RoomData : MonoBehaviour
{


    public Transform upSpawn;
    public Transform downSpawn;
    public Transform specialSpawn;

    public Transform baseCameraPos;

    private int enemyCount;

    public int EnemyCount
    {
        get
        {
            return enemyCount;
        }

        set
        {
            this.enemyCount = value;
            if (upStairUnlocked == false || downStairUnlocked == false)
            {
                if (enemyCount == 0)
                {
                    upStairUnlocked = true;
                    downStairUnlocked = true;
                    Destroy(upStairLock);
                    Destroy(downStairLock);
                }
            }
        }
    }

    public bool upStairUnlocked;
    public bool downStairUnlocked;

    public GameObject upStairLock;
    public GameObject downStairLock;

    private GameObject copperlockPrefab;

    private void Start()
    {
        copperlockPrefab = GameManager.Instance.copperlock;
        upStairLock = Instantiate(copperlockPrefab, downSpawn.position, Quaternion.identity);
        downStairLock = Instantiate(copperlockPrefab, upSpawn.position, Quaternion.identity);
        EnemyCount = GetComponentsInChildren<EnemyController>().Length;
        upStairLock.transform.parent = downSpawn;
        downStairLock.transform.parent = upSpawn;

        if (!GameManager.Instance.enteredSpecial && GameManager.Instance.StageCount == 0)
        {
            Destroy(upSpawn.gameObject);
        }
        
    }
} 
