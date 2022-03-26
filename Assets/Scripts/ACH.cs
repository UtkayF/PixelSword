using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class ACH : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [Header("Player")]
    public int Player_health;
    public bool Player_death;
    public int RandomXDamageForPlayerLOW;
    public int RandomYDamageForPlayerHEIGHT;

    [Header("Other Character")]
    public int FlyingAttacker_health;
    public int GoblinPlayer_health;
    public int MushroomPlayer_health;
    public int FantasyWarrior_health;
    public int Viking_health;
    public int DemonAxeRed_health;
    public int MWarrior_health;

    [Header("Player Health System")]
    public GameObject health1;
    public bool health_1;

    public GameObject health2;
    public bool health_2;

    public GameObject health3;
    public bool health_3;

    [Header("Magic or Spell")]
    public int Fireball_Count;
    public GameObject FBG;
    public GameObject fireballPrefabs;
    public GameObject spellHere;
    public Button Fireball_BTN;
    public TextMeshProUGUI FireballCOUNT_TEXT_BUTTONS;
    public TextMeshProUGUI FireballCOUNT_TEXT_SWIPE;

    [Header("Player Object")]
    public GameObject player;
    public bool stageButtonClose;


    [Header("Per Money")]
    public int perMoneyValue;

    [Header("Random Stage for Enemy")]
    public int Stage;
    public int RandomX;
    public int RandomY;

    [Header("Stage Handler")]
    public int DeadEnemyCounter;
    public int IncomingEnemy;
    public bool NewEnemiesWanted;
    public Button go_BTN;

    [Header("Pause Button")]
    public Button pauseBTN;

    [Header("Pause Menu CONTENT")]
    public Button resumeBTN;
    public Button homeBTN;
    public Button restartBTN;
    public GameObject PauseMenu;

    [Header("Dead Menu CONTENT")]
    public Button homeBTN_DIE;
    public Button restartBTN_DIE;
    public Button continueBTN_DIE;
    public GameObject DeadMenu;

    [Header("ADS")]
    [SerializeField] string _androidAdUnitId = "Rewarded_Android";
    [SerializeField] string _iOsAdUnitId = "Rewarded_iOS";
    string _adUnitId;
    public int adsType;

    void Awake()
    {
        PlayerPrefs.SetInt("perMoney", perMoneyValue);
        RandomXDamageForPlayerLOW = PlayerPrefs.GetInt("attackMin", 2);
        RandomYDamageForPlayerHEIGHT = PlayerPrefs.GetInt("attackMax", 5);
        Fireball_Count = PlayerPrefs.GetInt("fireballCounter", 3);
        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;

        //Disable button until ad is ready to show
        //_showAdButton.interactable = false;
    }

    void Start()
    {
        

        Fireball_BTN.onClick.AddListener(m_Fireball);

        go_BTN.onClick.AddListener(goBTN);
        pauseBTN.onClick.AddListener(pauseBTN_METHOD);
        resumeBTN.onClick.AddListener(resume_METHOD);
        restartBTN.onClick.AddListener(restart_METHOD);
        homeBTN.onClick.AddListener(home_METHOD);
       

        //Dead MENU
        homeBTN_DIE.onClick.AddListener(home_METHOD);
        restartBTN_DIE.onClick.AddListener(restart_METHOD);
        continueBTN_DIE.onClick.AddListener(continue_METHOD);
        Time.timeScale = 1;

    }
    void Update()
    {

        FireballCOUNT_TEXT_BUTTONS.text = Fireball_Count.ToString();
        FireballCOUNT_TEXT_SWIPE.text = Fireball_Count.ToString();
        Fireball_Count = PlayerPrefs.GetInt("fireballCounter", 3);

        if (Player_death == true)
        {
            Fireball_BTN.gameObject.SetActive(false);
            DeadMenu.SetActive(true);
           // Time.timeScale = 0;
        }

        if (stageButtonClose)
            go_BTN.gameObject.SetActive(true);
        else
            go_BTN.gameObject.SetActive(false);



        if (player.GetComponent<Test>().controllerNumber == 1)
        {
            if (Fireball_Count == 0)
                Fireball_BTN.gameObject.SetActive(false);
            else
                Fireball_BTN.gameObject.SetActive(true);
        }
        else
        {
            if (Fireball_Count == 0)
                player.GetComponent<Test>().fireball_Buttons.gameObject.SetActive(false);
            else
                player.GetComponent<Test>().fireball_Buttons.gameObject.SetActive(true);
        }




    }


    public void m_Fireball() {

        if(Fireball_Count != 0) { 
            Fireball_Count--;
            PlayerPrefs.SetInt("fireballCounter", Fireball_Count);
            player.GetComponent<Animator>().Play("Player_spell");

            StartCoroutine(FireballSupport());

            if (player.GetComponent<Test>().controllerNumber == 1)
            {
                if (Fireball_Count == 0)
                    Fireball_BTN.gameObject.SetActive(false);
                else
                    Fireball_BTN.gameObject.SetActive(true);
            }
            else
            {
                if (Fireball_Count == 0)
                    player.GetComponent<Test>().fireball_Buttons.gameObject.SetActive(false);
                else
                    player.GetComponent<Test>().fireball_Buttons.gameObject.SetActive(true);
            }

        }

    }

    IEnumerator FireballSupport()
    {
        player.GetComponent<Test>().isAttacking = true;
        player.GetComponent<Test>().isBlock = false;
        player.GetComponent<BoxCollider2D>().size = new Vector2(1.266667f, 1.933333f);
        player.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.9666666f);
        yield return new WaitForSeconds(.3f);
        FBG = Instantiate(fireballPrefabs, spellHere.transform.position, Quaternion.Euler(0, 0, 0));
        FBG.GetComponent<Rigidbody2D>().velocity = new Vector2(5, 0);
        yield return new WaitForSeconds(.1f);
        player.GetComponent<Test>().isAttacking = false;
    }

    void goBTN()
    {
        go_BTN.gameObject.SetActive(false);
        Stage++;
        stageButtonClose = false;
        print("Stage > " + Stage);
        NewEnemiesWanted = true;
    }


    void pauseBTN_METHOD()
    {
        Time.timeScale = 0;
        PauseMenu.SetActive(true);
    }

    void resume_METHOD()
    {
        Time.timeScale = 1;
        PauseMenu.SetActive(false);
    }

    void restart_METHOD()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("SampleScene");
    }

    void home_METHOD()
    {
        adsType = 1;
        LoadAd();
    }

 

    void continue_METHOD()
    {

        
        adsType = 2;
        LoadAd();
        Time.timeScale = 1;

    }



    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
        ShowAd();
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            //  _showAdButton.onClick.AddListener(ShowAd);
            // Enable the button for users to click:
            //  _showAdButton.interactable = true;
        }
    }

    // Implement a method to execute when the user clicks the button.
    public void ShowAd()
    {
        // Disable the button: 
        //_showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            Debug.Log("Unity Ads Rewarded Ad Completed");
            // Grant a reward.
            if(adsType == 2) {
                player.GetComponent<SpriteRenderer>().color = Color.white;
                DeadMenu.SetActive(false);
                player.SetActive(true);

                if (player.GetComponent<Test>().controllerNumber == 1)
                {
                    if (Fireball_Count == 0)
                        Fireball_BTN.gameObject.SetActive(false);
                    else
                        Fireball_BTN.gameObject.SetActive(true);
                }
                else
                {
                    if (Fireball_Count == 0)
                        player.GetComponent<Test>().fireball_Buttons.gameObject.SetActive(false);
                    else
                        player.GetComponent<Test>().fireball_Buttons.gameObject.SetActive(true);
                }


                Player_death = false;
                Player_health = 3;
                health_1 = true;
                health_2 = true;
                health_3 = true;

                health1.SetActive(true);
                health2.SetActive(true);
                health3.SetActive(true);
                


            }
            else
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("MenuScene");
            }
            
            Advertisement.Load(_adUnitId, this);
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
        if(adsType == 1)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("MenuScene");
        }

    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        //_showAdButton.onClick.RemoveAllListeners();
    }



}
