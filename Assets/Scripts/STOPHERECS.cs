using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class STOPHERECS : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rg2d;
    public SpriteRenderer spriteRenderer;
    public GameObject otherAttackBox;
    public ACH hSystem;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "hitBox")
        {
            print("Hedef konumunda.");
            print(collision.gameObject.name);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            animator = collision.gameObject.GetComponent<Animator>();
            rg2d = collision.gameObject.GetComponent<Rigidbody2D>();
            spriteRenderer = collision.gameObject.GetComponent<SpriteRenderer>();
            otherAttackBox = GameObject.Find("otherAttackBox");

            StartCoroutine(AttackAnim());

        }
    }


    IEnumerator AttackAnim()
    {
        againAttack:
                otherAttackBox.SetActive(true);
                animator.Play("Skeleton_idle");
                yield return new WaitForSeconds(3f);

                animator.Play("Skeleton_attack");
                yield return new WaitForSeconds(.6f);
                otherAttackBox.SetActive(false);

                yield return new WaitForSeconds(.6f);
                animator.Play("Skeleton_idle");
                otherAttackBox.SetActive(true);
        if (hSystem.Player_death == false)
        {
            goto againAttack;
        }
        else { }
    }

}
