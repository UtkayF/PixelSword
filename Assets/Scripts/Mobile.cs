using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobile : MonoBehaviour
{

    public GameObject cube;
    private GameObject touchedObj;

    private void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch inputTouch = Input.GetTouch(0);
            Vector2 touchPosition = Camera.main.ScreenToWorldPoint(inputTouch.position);
            //Instantiate(cube, touchPosition, Quaternion.identity);

            // TOUCH BEGAN
            if(inputTouch.phase == TouchPhase.Began)
            {
                Collider[] touchCollider = Physics.OverlapSphere(touchPosition, 1f);

                if (touchCollider.Length > 0)
                    touchedObj = touchCollider[0].gameObject;

            }


            //TOUCH MOVED
            if(inputTouch.phase == TouchPhase.Moved)
            {
                if(touchedObj != null)
                    touchedObj.transform.position = touchPosition;
            }

            if (inputTouch.phase == TouchPhase.Ended)
            {
                if(touchedObj != null)
                    touchedObj = null;
            }


        }

    }

}
