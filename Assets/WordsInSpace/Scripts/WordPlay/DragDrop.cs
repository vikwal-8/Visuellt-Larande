using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameManager gameManagerScript;
    public Transform parentToReturnTo;
    public char value;
    public int id;

    private void Awake() {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = gameManagerScript.FindInActiveObjectByTag("WordPlayCanvas").GetComponent<Canvas>();
    }
    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
    } 
    public void OnDrag(PointerEventData eventData) {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;
        this.transform.SetParent(null);
        this.transform.SetParent(parentToReturnTo);
    }
    public void OnPointerDown(PointerEventData eventData) {
        Debug.Log("clicked object");
    }
    public void OnDrop(PointerEventData eventData) {
        
    }
}