using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Letter : MonoBehaviour
{
    public char value;
    private GameObject player, ingameGUI;
    private GameManager gameManagerScript;
    SpawnLetters spawner;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        ingameGUI = GameObject.FindGameObjectWithTag("WordlistGUI");
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        string str = RandomLetter();
        value = str.ToCharArray()[0];
        GetComponent<TextMeshPro>().text = str;
        

    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - transform.position).sqrMagnitude > 1) //Undviker hetsig rotation om man åker väldigt nära den.
        {
            RotateToCameraAngle();
        }
    }

    private string RandomLetter()
    {
        string[] Alphabet = new string[117] { "A", "A", "A", "A", "A", "A", "A", "A", "A", "A", "B", "B", "B", "C", "D", "D", "D", "D", "D", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "E", "F", "F", "G", "G", "G", "H", "H", "I", "I", "I", "I", "I", "I", "J", "J", "J", "K", "K", "K", "L", "L", "L", "L", "L", "M", "M", "M", "N", "N", "N", "N", "N", "N", "N", "N", "O", "O", "O", "O", "P", "P", "Q", "R", "R", "R", "R", "R", "R", "R", "R", "S", "S", "S", "S", "S", "S", "S", "T", "T", "T", "T", "T", "T", "T", "T", "U", "U", "V", "V", "W", "X", "Y", "Z", "Å", "Å", "Å", "Å", "Ä", "Ä", "Ä", "Ä", "Ä", "Ä", "Ö", "Ö", "Ö", "Ö" }; //Multiple entries to match words letter frequncy.
        return Alphabet[Random.Range(0, Alphabet.Length)];
    }

    public void GetSpawner(SpawnLetters spawner)
    {
        this.spawner = spawner;
    }


    private void RotateToCameraAngle()
    {
        transform.LookAt(player.transform.position);
        transform.rotation *= Quaternion.Euler(0, 180, 0);
    }

    private void OnTriggerEnter()
    {
        gameManagerScript.AddToArray(value);
        string temp = ingameGUI.GetComponent<TextMeshProUGUI>().text;
        ingameGUI.GetComponent<TextMeshProUGUI>().text = temp + " " + value;
        spawner.spawnLetter();
        Destroy(gameObject);
    }
}
