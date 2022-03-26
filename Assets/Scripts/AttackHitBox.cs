using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AttackHitBox : MonoBehaviour
{

    public GameObject expParticle;
    private int translateMoney;
    private int currentMoney;
    private int perMoney;

    public SpawnOtherPlayer spawnClass;
    public Test testClass;
    public ACH hSystem;
    private int damageValue;
    private int randomNumber;
    private int x;
    private int y;

    public SpriteRenderer otherCharSprite;
    private bool redToWhite;

    private void Start()
    {
        perMoney = PlayerPrefs.GetInt("perMoney", 3);
        x = PlayerPrefs.GetInt("attackMin", 2);
        y = PlayerPrefs.GetInt("attackMax", 5);
    }

    private void Update()
    {
        

        if (redToWhite == true)
            StartCoroutine(redToWhiteMethod());
    }

    IEnumerator redToWhiteMethod()
    {
        yield return new WaitForSeconds(0.0000001f);
        if(otherCharSprite != null)
            otherCharSprite.color = Color.white;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "hitBox")
        {
            collision.gameObject.GetComponent<Animator>().Play("Skeleton_damage");
            otherCharSprite = collision.gameObject.GetComponent<SpriteRenderer>();
            redToWhite = true;
            otherCharSprite.color = Color.red;

            if (collision.gameObject.name == "FlyingAttacker(Clone)")
            {
                randomNumber = Random.Range(x, y);
                damageValue = damageValue + randomNumber;
                if(damageValue >= hSystem.FlyingAttacker_health)
                {
                    Destroy(collision.gameObject);
                    spawnClass.enemyExists = false; // DJKANWDWANLKDMLAKWDMKLAWMGAMWGAW
                    Instantiate(expParticle, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    moneySystem();
                }
            }

            if (collision.gameObject.name == "GoblinPlayer(Clone)")
            {
                randomNumber = Random.Range(x, y);
                damageValue = damageValue + randomNumber;
                if (damageValue >= hSystem.GoblinPlayer_health)
                {
                    Destroy(collision.gameObject);
                    spawnClass.enemyExists = false; // DJKANWDWANLKDMLAKWDMKLAWMGAMWGAW
                    Instantiate(expParticle, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    moneySystem();
                }
            }

            if (collision.gameObject.name == "MushroomPlayer(Clone)")
            {
                randomNumber = Random.Range(x, y);
                damageValue = damageValue + randomNumber;
                if (damageValue >= hSystem.MushroomPlayer_health)
                {
                    Destroy(collision.gameObject);
                    spawnClass.enemyExists = false; // DJKANWDWANLKDMLAKWDMKLAWMGAMWGAW
                    Instantiate(expParticle, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    moneySystem();
                }
            }

            if (collision.gameObject.name == "FantasyWarrior(Clone)")
            {
                randomNumber = Random.Range(x, y);
                damageValue = damageValue + randomNumber;
                if (damageValue >= hSystem.FantasyWarrior_health)
                {
                    Destroy(collision.gameObject);
                    spawnClass.enemyExists = false; // DJKANWDWANLKDMLAKWDMKLAWMGAMWGAW
                    Instantiate(expParticle, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    moneySystem();
                }
            }

            if (collision.gameObject.name == "Viking(Clone)")
            {
                randomNumber = Random.Range(x, y);
                damageValue = damageValue + randomNumber;
                if (damageValue >= hSystem.Viking_health)
                {
                    Destroy(collision.gameObject);
                    spawnClass.enemyExists = false; // DJKANWDWANLKDMLAKWDMKLAWMGAMWGAW
                    Instantiate(expParticle, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    moneySystem();
                }
            }

            if (collision.gameObject.name == "DemonAxeRed(Clone)")
            {
                randomNumber = Random.Range(x, y);
                damageValue = damageValue + randomNumber;
                if (damageValue >= hSystem.DemonAxeRed_health)
                {
                    Destroy(collision.gameObject);
                    spawnClass.enemyExists = false; // DJKANWDWANLKDMLAKWDMKLAWMGAMWGAW
                    Instantiate(expParticle, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    moneySystem();
                }
            }

            if (collision.gameObject.name == "MWarrior(Clone)")
            {
                randomNumber = Random.Range(x, y);
                damageValue = damageValue + randomNumber;
                if (damageValue >= hSystem.MWarrior_health)
                {
                    Destroy(collision.gameObject);
                    spawnClass.enemyExists = false; // DJKANWDWANLKDMLAKWDMKLAWMGAMWGAW
                    Instantiate(expParticle, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                    moneySystem();
                }
            }



        }
    }

    void moneySystem() {
        translateMoney = perMoney;
        currentMoney = PlayerPrefs.GetInt("money");
        currentMoney = currentMoney + translateMoney;
        PlayerPrefs.SetInt("money", currentMoney);
        translateMoney = 0;
        damageValue = 0;
        otherCharSprite = null;
    }

}
