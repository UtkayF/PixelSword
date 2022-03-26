using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forPlayerCollision : MonoBehaviour
{

    public GameObject offSetChanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "turnEnd")
        {
            offSetChanger.GetComponent<CameraFollow>().xOffSet = -2f;
            this.GetComponent<SpriteRenderer>().flipX = true;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.8f,0);
        }


        if (collision.gameObject.tag == "turnStart")
        {
            offSetChanger.GetComponent<CameraFollow>().xOffSet = 2f;
            this.GetComponent<SpriteRenderer>().flipX = false;
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(1.8f, 0);
        }

    }

}
