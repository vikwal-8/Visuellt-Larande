using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{

    private static GameAssets _i;

    public static GameAssets i
    {
        get
        {
            if (_i == null) _i = (Instantiate(Resources.Load("GameAssets")) as GameObject).GetComponent<GameAssets>();
            return _i;

        }
    }

    public GameObject House1;
    public GameObject House2;
    public GameObject House3;
    public GameObject farm;
    public Sprite PlaceHolderSprite1;
    public Sprite PlaceHolderSprite2;
    public Sprite PlaceHolderSprite3;
    public Sprite PlaceHolderSprite4;

}
