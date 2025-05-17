using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed;

    [SerializeField]
    private Vector3 offset;

    RoomData roomData;

    public Vector3 basePosition;

    private void Start()
    {
        target = GameManager.Instance.player.transform;
        basePosition = target.position;
        transform.position = basePosition + offset;
    }

    private void LateUpdate()
    {

        if(roomData != GameManager.Instance.nowRoomData)
        {
            roomData = GameManager.Instance.nowRoomData;
            basePosition = roomData.baseCameraPos.position;
            transform.position = basePosition + offset;
        }

        float distanceY = Mathf.Abs(transform.position.y - target.position.y);
        if (target.position.y > basePosition.y || distanceY > 4f)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.x = transform.position.x;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }

        float distanceX = Mathf.Abs(transform.position.x - target.position.x);
        if (distanceX >= 2)
        {
            Vector3 desiredPosition = target.position + offset;
            desiredPosition.y = transform.position.y;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }



        
    }
}
