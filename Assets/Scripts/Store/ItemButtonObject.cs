using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// This class is only a data-type to hold button and the itemtype associated with it
public class ItemButtonObject
{

    private Item.ItemType itemType;
    private Button button;

    public ItemButtonObject(Item.ItemType itemType, Button button){
        this.itemType = itemType;
        this.button = button;
        }


    public Button GetButton()
    {
        return button;
    }

    public Item.ItemType GetItemType()
    {
        return itemType;
    }

}
