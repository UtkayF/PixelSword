using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{

    public Animator animator;
    private bool animationOk = false;

    private int translateMoney;
    private int currentMoney;
    private int perMoney;

    public GameObject expParticle;
    public GameObject spawnClass;
    public GameObject STOPHERECS;
    public GameObject player;

    private void Start()
    {
        spawnClass = GameObject.Find("GameMANAGER");
        STOPHERECS = GameObject.Find("STOPHERE");
        player = GameObject.Find("Player");
    }

    private void Update()
    {

        perMoney = PlayerPrefs.GetInt("perMoney", 3);

        if (animationOk)
        {
            StartCoroutine(deathFIREBALL());
        }
       
    }

    IEnumerator deathFIREBALL()
    {
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
        translateMoney = perMoney;
        currentMoney = PlayerPrefs.GetInt("money");
        currentMoney = currentMoney + translateMoney;
        PlayerPrefs.SetInt("money", currentMoney);
        translateMoney = 0;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "hitBox")
        {
            Destroy(collision.gameObject);
            STOPHERECS.GetComponent<STOPHERECS>().animator = null;
            STOPHERECS.GetComponent<STOPHERECS>().rg2d = null;
            STOPHERECS.GetComponent<STOPHERECS>().spriteRenderer = null;
            STOPHERECS.GetComponent<STOPHERECS>().otherAttackBox = null;
            spawnClass.GetComponent<SpawnOtherPlayer>().enemyExists = false;// DJKANWDWANLKDMLAKWDMKLAWMGAMWGAW
            Instantiate(expParticle, collision.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
            animator.Play("fireball_death");
            animationOk = true;
            this.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
        }

        if(collision.gameObject.tag == "hitBoxWALL")
        {
            Destroy(gameObject);
        }
    }
    
   
}
