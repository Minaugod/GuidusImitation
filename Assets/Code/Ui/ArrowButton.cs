using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class ArrowButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public Direction direction;
    private Image buttonImage;
    private Color originalColor;
    public Color clickColor;

    private CharacterController player;

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
        originalColor = buttonImage.color;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        buttonImage.color = clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonImage.color = originalColor;
        player.ArrowBtnClick(direction);

    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ü���� �ɾ �� �Լ��� �� ������ ȣ��ȴ�.
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameManager.Instance.player;
    }


}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}
