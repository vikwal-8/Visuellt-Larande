using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour,IDropHandler
{

    
    void Start()
    {
        foreach (Transform eachChild in transform)
        {
            //Debug.Log(eachChild.GetComponent<TextMeshProUGUI>().text);
        }
    }
    public void OnDrop(PointerEventData eventData) {
        Debug.Log("OnDrop");
        DragDrop d = eventData.pointerDrag.GetComponent<DragDrop>();
        if (eventData.pointerDrag != null) {
            d.parentToReturnTo = this.transform;
        }
    }
}
