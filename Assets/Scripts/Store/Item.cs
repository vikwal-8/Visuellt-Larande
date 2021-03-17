using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        HouseNone,
        House_1,
        House_2,
        House_3,
        farm
    }
    //Gets the cost of the item
    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
        default:
        case ItemType.farm:         return 1;
        case ItemType.House_1:      return 999;
        case ItemType.House_2:      return 4999;
        case ItemType.House_3:      return 9999;

        }
    }
        //GETS THE sprite FROM GAMEASSETS
        public static Sprite GetSprite(ItemType itemType)
        {
            switch (itemType)
            {
            default:
            case ItemType.farm:         return GameAssets.i.PlaceHolderSprite1;
            case ItemType.House_1:      return GameAssets.i.PlaceHolderSprite1;
            case ItemType.House_2:      return GameAssets.i.PlaceHolderSprite2;
            case ItemType.House_3:      return GameAssets.i.PlaceHolderSprite3;
            }
        }


    public static string GetTagName(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.farm: return "BuyItem4";
            case ItemType.House_1: return "BuyItem1";
            case ItemType.House_2: return "BuyItem2";
            case ItemType.House_3: return "BuyItem3";
        }
    }

    public static string GetName(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.farm: return "Trädgårdsland";
            case ItemType.House_1: return "Hus 1";
            case ItemType.House_2: return "Hus 2";
            case ItemType.House_3: return "Hus 3";
        }
    }


}
