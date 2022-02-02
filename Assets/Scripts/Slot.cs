using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(EventTrigger))]
public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] private GameController controller;
    [SerializeField] private TextMeshProUGUI textcost;
    private Vector3 oldPos;
    private PointerEventData pointerEventData;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    /// <summary>
    /// Кому принадлежит вещь
    /// </summary>
    [SerializeField] private int parent;

    [SerializeField] private int cost;


    public event Action<int,int> onBuy;

    private void Update()
    {
        textcost.text = Convert.ToString(cost);
    }

    private void OnEnable()
    {
        raycaster = gameObject.GetComponent<GraphicRaycaster>();
        eventSystem = FindObjectOfType<EventSystem>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        gameObject.transform.DOScale(Vector3.one * 1.2f, 0.3f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        gameObject.transform.DOScale(Vector3.one, 0.3f);
    }

    public void OnDrag(PointerEventData eventData)
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        transform.position = pos;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // try
        // {
            pointerEventData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            var result = new List<RaycastResult>();
            raycaster.Raycast(pointerEventData, result);
            Debug.Log(1);
            if (result.Count == 0)
            {
                transform.position = oldPos;
                return;
            }

            if (result[0].gameObject.GetComponent<Slot>().parent == parent)
            {
                transform.position = oldPos;
                return;
            }

            if (result[0].gameObject.GetComponent<Slot>().cost < controller.GetCountMoney(parent))
            {
                if (cost == 0)
                {
                    onBuy?.Invoke(parent, result[0].gameObject.GetComponent<Slot>().cost);
                }
                else
                {
                    onBuy?.Invoke(result[0].gameObject.GetComponent<Slot>().parent, cost);
                }

                var tempImage = result[0].gameObject.GetComponent<Image>().sprite;
                var tempcost = result[0].gameObject.GetComponent<Slot>().cost;
                result[0].gameObject.GetComponent<Image>().sprite = GetComponent<Image>().sprite;
                result[0].gameObject.GetComponent<Slot>().cost = cost;
                GetComponent<Image>().sprite = tempImage;
                cost = tempcost;
            }
            transform.position = oldPos;
        // }
        // catch
        // {
        //     transform.position = oldPos;
        // }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oldPos = gameObject.transform.position;
    }
}
