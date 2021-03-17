using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour
{
    public static UI_Shop test;
    private Transform container;
    private Transform shopItemTemplate;
    private IShopCustomer shopCustomer;
    //private Transform buttontest;
   // private bool h1;
    //private bool h2;
    //private bool h3;
    public int credits;
    public Button b1;
    public Button b2;
    public Button b3;
    public Button b4;
    private List<ItemButtonObject> buttonlist = new List<ItemButtonObject>();
    SaveData temp;

    private void Awake() 
    {
        test = this;
        container = transform.Find("container");
        shopItemTemplate = container.Find("shopItemTemplate");
        shopItemTemplate.gameObject.SetActive(false);

    }
    private void Start()
    {
        CreateItemButton(Item.ItemType.House_1, 0);
        CreateItemButton(Item.ItemType.House_2, 1);
        CreateItemButton(Item.ItemType.House_3, 2);
        CreateItemButton(Item.ItemType.farm, 3);
        ShowOrHideButton();
        LoadSave();

        //Hide();
    }
    private void CreateItemButton(Item.ItemType itemType,  int positionIndex)
    {
        Transform shopItemTransform = Instantiate(shopItemTemplate, container);
        shopItemTransform.gameObject.SetActive(true);
        RectTransform shopItemRectTransform = shopItemTransform.GetComponent<RectTransform>();

        float shopItemHeight = 75f;
        shopItemRectTransform.anchoredPosition = new Vector2(0, -shopItemHeight * positionIndex);

        shopItemTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(Item.GetName(itemType));
        shopItemTransform.Find("costText").GetComponent<TextMeshProUGUI>().SetText(Item.GetCost(itemType).ToString());
        shopItemTransform.Find("itemImage").GetComponent<Image>().sprite = Item.GetSprite(itemType);
        Button button = shopItemTransform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(() => { TryBuyItem(positionIndex, button); });
        buttonlist.Add(new ItemButtonObject(itemType, button));
    }
    public void Show()
    {
        //this.shopCustomer = shopCustomer;
        LoadSave();
        gameObject.SetActive(true);
    }
    public void Hide(){

        gameObject.SetActive(false);
    }
    public void TryBuyItem(int pos, Button button)
    {
        if (pos == 0)
        {
            BuyItem(Item.ItemType.House_1, button);
        }
        if (pos == 1)
        {
            BuyItem(Item.ItemType.House_2, button);
        }
        if (pos == 2)
        {
            BuyItem(Item.ItemType.House_3, button);
        }
        if (pos == 3)
        {
            BuyItem(Item.ItemType.farm, button);
        }
    }


    public void BuyItem(Item.ItemType type, Button button)
    {
        
        temp.enabledObjects.Add(Item.GetTagName(type));
        temp.credits = temp.credits - Item.GetCost(type);
        WriteSave();
        GameManagerMainWorld.PlaceBuyItems();
        GameManagerMainWorld.UpdateDisplayedCoins();
        ShowOrHideButton();
    }


    private void ShowOrHideButton()
    {
        foreach (ItemButtonObject obj in buttonlist)
        {
            
            if (temp.credits < Item.GetCost(obj.GetItemType()) || (temp.enabledObjects.Contains(Item.GetTagName(obj.GetItemType()))))
            {
                obj.GetButton().gameObject.SetActive(false);
            }
        }

    }

    public void LoadSave()
    {
        temp = SaveSystem.LoadSave();
    }
    public void WriteSave()
    {
        SaveSystem.Save(temp);
    }


}
