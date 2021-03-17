using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlotDropZone : MonoBehaviour, IDropHandler
{

    private WordPlay wordPlay;
    void Start()
    {
        wordPlay = transform.parent.gameObject.GetComponent<WordPlay>();
        foreach (Transform eachChild in transform)
        {
            Debug.Log(eachChild.GetComponent<TextMeshProUGUI>().text);
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        DragDrop d = eventData.pointerDrag.GetComponent<DragDrop>();
        if (eventData.pointerDrag != null)
        {
            d.parentToReturnTo = this.transform;
            StartCoroutine(SendUpdate(d));
        }


    }

    IEnumerator SendUpdate(DragDrop d)
    {   

        bool isChildYet = false;
        while (!isChildYet) {
            Debug.Log(transform.Cast<Transform>().ToList().Count);
            foreach (Transform eachChild in transform.Cast<Transform>().ToList())
            {
                //Debug.Log("Söker " + d.id + " Kollar nu " + eachChild.gameObject.GetComponent<DragDrop>().id);
                if (d.id == eachChild.gameObject.GetComponent<DragDrop>().id)
                {
                    isChildYet = true;
                }
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }

        wordPlay.Check();
    }
}
