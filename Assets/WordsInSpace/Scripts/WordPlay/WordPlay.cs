using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WordPlay : MonoBehaviour
{
    public int points;
    private GameManager gameManagerScript;
    private List<char> letterList;
    public GameObject letterPrefab;
    private GameObject letterSlot, letterDropZone, pointText;
    private static int id;
    private string urlAPI = "https://skrutten.csc.kth.se/granskaapi/spell/json/";
    private RootJsonObject temp;
    private string[] BlockedWords =  { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", 
        "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Ä" };//Tillåter att blockera vissa ord. Bör implemeteras mot filsystem som en textfil.
    public List<string> usedWords = new List<string>();

    // Start is called before the first frame update
    void Awake()
    {
        gameManagerScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        letterSlot = transform.Find("LetterHolder").gameObject;
        letterDropZone = transform.Find("ItemSlotHolder").gameObject;
        pointText = transform.Find("Points").gameObject;

        letterSlot.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height / 2);
        letterDropZone.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Screen.height / 2);
        points = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaceAllLetters()
    {
        letterList = gameManagerScript.letterList;
      
    

        foreach (char letter in letterList)
        {
            Create(letter);

        }

    }
    public void ResetPosition()
    {
        foreach (Transform eachChild in letterDropZone.transform.Cast<Transform>().ToList())
        {
            Debug.Log(eachChild.GetComponent<TextMeshProUGUI>().text);
            eachChild.transform.SetParent(letterSlot.transform);
        }

    }

    public void PrintCurrent()
    {
        string str = "";
        foreach (Transform eachChild in letterDropZone.transform.Cast<Transform>().ToList())
        {
            str+= eachChild.GetComponent<TextMeshProUGUI>().text;
        }
        Debug.Log(str);
    }

    public void Check()
    {
        string str = "";
        foreach (Transform eachChild in letterDropZone.transform.Cast<Transform>().ToList())
        {
            str += eachChild.GetComponent<TextMeshProUGUI>().text;
        }
        if (str.Length >= 1 && !BlockedWords.Contains(str) && !usedWords.Contains(str)) //API ger rätt för enkla bokstäver, tar bort detta bortsett från Å och Ö.
        {
            StartCoroutine(CheckWordSpelling(str));
        }
    }

    private void AddPoints(int points)
    {
        this.points += points*10;
        pointText.GetComponent<TextMeshProUGUI>().text = (" Poäng: "+this.points.ToString());
        
    }

    public void Create(char value)
    {
        GameObject newObject = Instantiate(letterPrefab) as GameObject;
        DragDrop yourObject = newObject.GetComponent<DragDrop>();
        yourObject.value = value;
        yourObject.transform.SetParent(letterSlot.transform);
        yourObject.GetComponent<TextMeshProUGUI>().text = value.ToString();
        yourObject.id = id++;
    }



    public IEnumerator CheckWordSpelling(string word)
    {
        string uri = urlAPI + word.ToLower();
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;
            Debug.Log(uri);
            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                
                temp = JsonUtility.FromJson<RootJsonObject>("{\"jsonObject\":" + webRequest.downloadHandler.text + "}");
                Debug.Log(word + " " + temp.jsonObject[0].correct);
                if (temp.jsonObject[0].correct)
                {
                    AddPoints(word.Length);
                    usedWords.Add(word);
                }
            }
        }
    }

    public void Exit(){
        if(points > 0){ gameManagerScript.Exit(points); }
        else {gameManagerScript.Exit(0);}
        
    }
    public void ReRun()
    {
        if (points > 0) { gameManagerScript.RestartGame(points); }
        else { gameManagerScript.RestartGame(0); }
    }

}

[System.Serializable]
public class RootJsonObject
{
    public JsonObject[] jsonObject;
}

[System.Serializable]
public class JsonObject
{
    public string word;
    public string[] suggestions;
    public bool correct;

    public static JsonObject CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<JsonObject>(jsonString);
    }
}
