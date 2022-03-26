using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManagers : MonoBehaviour
{

    public TextMeshProUGUI MoneyValueTEXT;
    public TextMeshProUGUI StageTEXT;
    public int currentMoney;
    public int gameMusic;

    public ACH ach;
    public GameObject songs;

    private void Start()
    {
        print(PlayerPrefs.GetInt("money"));
        gameMusic = PlayerPrefs.GetInt("gameMusic", 1);
    }

    private void Update() 
    {
        currentMoney = PlayerPrefs.GetInt("money");
        MoneyValueTEXT.text = prettyCurrency(currentMoney);
        StageTEXT.text = ach.Stage + " Level";

        gameMusic = PlayerPrefs.GetInt("gameMusic", 1);

        if (gameMusic == 1)
            songs.SetActive(true);
        else
            songs.SetActive(false);
    }


    static readonly string[] suffixes = { "", "k", "M", "G" };
    static string prettyCurrency(long cash)
    {
        int k;
        if (cash == 0)
            k = 0;    // log10 of 0 is not valid
        else
            k = (int)(Mathf.Log10(cash) / 3); // get number of digits and divide by 3
        var dividor = Mathf.Pow(10, k * 3);  // actual number we print
        var text = (cash / dividor).ToString("F") + suffixes[k];
        return text;
    }

}
