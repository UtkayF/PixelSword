using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fakeGround : MonoBehaviour
{
    public Test jumpAttackSupport;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "player") { 
            jumpAttackSupport.fakeGround = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            jumpAttackSupport.fakeGround = true;
        }
    }

}
