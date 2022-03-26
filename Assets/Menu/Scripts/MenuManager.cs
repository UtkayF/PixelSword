using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject player;

    [Header("Menu Buttons")]
    public Button playBTN;
    public Button shopBTN;
    public Button settingsBTN;

    [Header("All Of Menu Layer")]
    public GameObject AllOfMenuLayer;

    [Header("Shop Layer")]
    public GameObject shopLayer;
    public Button shopExitBTN;

    [Header("Settings Layer")]
    public GameObject settingsLayer;
    public Button settingsExitBTN;

    [Header("Settings Content")]
    public Button GameMusicBTN;
    public bool gameMusicBool;
    public int gameMusic; //1 - 0
    public Sprite gameMusicOFF; 
    public Sprite gameMusicON;

    [Header("Controllers Content")]
    public Button ControllerLeftBTN;
    public TextMeshProUGUI ControllerINFO;
    public Button ControllerRightBTN;
    public bool swipeOrButtonsBOOL;
    public int swipeOrButtonsINT; //1 T - 0 F
    // 1 = swipe true
    // 0 = buttons false


    [Header("Shop Layer CONTENT")]
    public Sprite maxSprite;
    public TextMeshProUGUI Strenght_Text;
    public Button StrenghtBuyButton;
    public TextMeshProUGUI Fireball_Text;
    public Button FireballBuyButton;
    public TextMeshProUGUI Money_T_TEXT;
    public TextMeshProUGUI FireballCOUNT_TEXT;

    [Header("Character properties handler with money")]
    public int attackMinINT;
    public int attackMaxINT;
    public int fireballCountINT;
    public int moneyValue;
    public bool buyFireballBOOL;
    public bool buyStrenghtBOOL;

    



    private void Start()
    {

        Time.timeScale = 1;
        gameMusic = PlayerPrefs.GetInt("gameMusic", 1);
        swipeOrButtonsINT = PlayerPrefs.GetInt("controller", 1);

        attackMinINT = PlayerPrefs.GetInt("attackMin", 2);
        attackMaxINT = PlayerPrefs.GetInt("attackMax", 5);

        fireballCountINT = PlayerPrefs.GetInt("fireballCounter", 3);
        moneyValue = PlayerPrefs.GetInt("money", 0);
        

        if (swipeOrButtonsINT == 1)
            swipeOrButtonsBOOL = true;
        else
            swipeOrButtonsBOOL = false;

        //GameMusic HANDLER
        if (gameMusic == 1)
            gameMusicBool = true;
        else
            gameMusicBool = false;



        player.GetComponent<Animator>().Play("Player_run");
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(1.8f,0f);

        playBTN.onClick.AddListener(playMETHOD);
        shopBTN.onClick.AddListener(shopMETHOD);
        settingsBTN.onClick.AddListener(settingsMETHOD);

        //EXIT TO LAYER
        settingsExitBTN.onClick.AddListener(settingsExitMETHOD);
        shopExitBTN.onClick.AddListener(shopExitMETHOD);

        //Settings Layer
        GameMusicBTN.onClick.AddListener(GameMusicMETHOD);
     

        //Swipe - Buttons
        ControllerLeftBTN.onClick.AddListener(GameControllersMethod);
        ControllerRightBTN.onClick.AddListener(GameControllersMethod);

        //MARKET
        StrenghtBuyButton.onClick.AddListener(SBB);
        FireballBuyButton.onClick.AddListener(FBB);

    }

    private void Update()
    {
        gameMusic = PlayerPrefs.GetInt("gameMusic", 1);
        
        swipeOrButtonsINT = PlayerPrefs.GetInt("controller", 1);

        attackMinINT = PlayerPrefs.GetInt("attackMin", 2);
        attackMaxINT = PlayerPrefs.GetInt("attackMax", 5);

        fireballCountINT = PlayerPrefs.GetInt("fireballCounter", 3);
        moneyValue = PlayerPrefs.GetInt("money", 0);

        FireballCOUNT_TEXT.text = fireballCountINT.ToString();

        Money_T_TEXT.text = prettyCurrency(moneyValue);


        if(attackMinINT == 60 && attackMaxINT > 60)
        {
            StrenghtBuyButton.GetComponent<Image>().sprite = maxSprite;
            buyStrenghtBOOL = false;
            Strenght_Text.text = "Strength " + attackMinINT + "-" + attackMaxINT + "\nMax";
        }
        else
        {
            buyStrenghtBOOL = true;
            Strenght_Text.text = "Strength " + attackMinINT + "-" + attackMaxINT + "\nUpgrade Cost: 3.9k";
        }


        if (fireballCountINT >= 20)
        {
            FireballBuyButton.GetComponent<Image>().sprite = maxSprite;
            buyFireballBOOL = false;
            Fireball_Text.text = "Fireball\nMax";
        }
        else
        {
            buyFireballBOOL = true;
            Fireball_Text.text = "Fireball +1\nCost: 3.5k";
        }


        if (swipeOrButtonsINT == 1)
            swipeOrButtonsBOOL = true;
        else
            swipeOrButtonsBOOL = false;

        //GameMusic HANDLER
        if (gameMusic == 1)
            gameMusicBool = true;
        else
            gameMusicBool = false;

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

    void SBB()
    {
        if(buyStrenghtBOOL == true && moneyValue >= 3900)
        {
            moneyValue = moneyValue - 3900;
            PlayerPrefs.SetInt("attackMin",attackMinINT + 2);
            PlayerPrefs.SetInt("attackMax",attackMaxINT + 2);
            PlayerPrefs.SetInt("money", moneyValue); 
             
            
        }
    }

    void FBB()
    {
        if (buyFireballBOOL == true && moneyValue >= 3500)
        {
            moneyValue = moneyValue - 3500;
            PlayerPrefs.SetInt("fireballCounter", fireballCountINT+1);
            PlayerPrefs.SetInt("money", moneyValue);

        }
    }

    void playMETHOD()
    {
        SceneManager.LoadScene("SampleScene");
    }
    void shopMETHOD()
    {
        shopLayer.SetActive(true);
        AllOfMenuLayer.SetActive(false);


    }    
    void settingsMETHOD()
    {
        settingsLayer.SetActive(true);
        AllOfMenuLayer.SetActive(false);

        if (swipeOrButtonsBOOL)
            ControllerINFO.text = "Swipe";
        else
            ControllerINFO.text = "Buttons";

        //Icon handler
        if (gameMusicBool)
            GameMusicBTN.GetComponent<Image>().sprite = gameMusicON;
        else
            GameMusicBTN.GetComponent<Image>().sprite = gameMusicOFF;


    }

    //EXIT BUTTONS
    void shopExitMETHOD()
    {
        shopLayer.SetActive(false);
        AllOfMenuLayer.SetActive(true);
    }
    void settingsExitMETHOD()
    {
        settingsLayer.SetActive(false);
        AllOfMenuLayer.SetActive(true);
    }


    void GameMusicMETHOD()
    {
        gameMusicBool = !gameMusicBool;
        if (gameMusicBool) { 
            GameMusicBTN.GetComponent<Image>().sprite = gameMusicON;
            PlayerPrefs.SetInt("gameMusic", 1);
        }
        else { 
            GameMusicBTN.GetComponent<Image>().sprite = gameMusicOFF;
            PlayerPrefs.SetInt("gameMusic", 0);
        }
    }


    void GameControllersMethod()
    {
        swipeOrButtonsBOOL = !swipeOrButtonsBOOL;
        if (swipeOrButtonsBOOL) { 
            ControllerINFO.text = "Swipe";
            PlayerPrefs.SetInt("controller", 1);
        }
        else
        { 
            ControllerINFO.text = "Buttons";
            PlayerPrefs.SetInt("controller", 0);
        }
    }



}
