using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherAttack : MonoBehaviour
{

    public SpriteRenderer sprite;
    public GameObject player;
    public ACH hSystem;
    public GameObject hSystemGAMEOBJ;
    public GameObject expParticle;
    public GameObject jumpAttackSupport;
    public DeadInterstitialAd deadAds;

    private void Start()
    {
        player = GameObject.Find("Player");
        sprite = player.GetComponent<SpriteRenderer>();

        hSystemGAMEOBJ = GameObject.Find("GameMANAGER");
        hSystem = hSystemGAMEOBJ.GetComponent<ACH>();

        deadAds = GameObject.Find("DeadAdsOBJ").GetComponent<DeadInterstitialAd>();

        jumpAttackSupport = GameObject.Find("Player");

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "player" && player.GetComponent<Test>().isBlock == false && player.GetComponent<Test>().isAttacking == false && player.GetComponent<Test>().isGrounded == true && jumpAttackSupport.GetComponent<Test>().fakeGround == true)
        {
            print("HASAR ALINDI");
            player.GetComponent<Test>().redToWhite = true;
            player.GetComponent<Animator>().Play("Player_damage");
            sprite = player.GetComponent<SpriteRenderer>();
            sprite.color = Color.red;
            
            player.GetComponent<BoxCollider2D>().offset = new Vector2(0f, 0.9666666f);
            player.GetComponent<BoxCollider2D>().size = new Vector2(1.266667f, 1.933333f);

            if (hSystem.health_3)
            {
                hSystem.health_3 = false;
                hSystem.health3.SetActive(false);
            }
            else if (hSystem.health_2)
            {
                hSystem.health_2 = false;
                hSystem.health2.SetActive(false);
            }
            else if (hSystem.health_1)
            {
                hSystem.health_1 = false;
                hSystem.health1.SetActive(false);
            }

            hSystem.Player_health--;
            if (hSystem.Player_health <= 0)
            {
                //Destroy(other.gameObject);
                other.gameObject.SetActive(false);
                Instantiate(expParticle, other.gameObject.transform.position, Quaternion.Euler(0, 0, 0));
                hSystem.Player_death = true;

                deadAds.LoadAd();

            }

        }

        if(other.gameObject.tag == "playerBlock" && player.GetComponent<Test>().isBlock == true)
        {
            print("Attack blocklandı");
        }
    }

}
