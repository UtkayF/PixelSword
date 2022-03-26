using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public float followSpeed = 2f;
    public Transform target;
    public float xOffSet = 1f;
    public float yOffSet = 1f;
   
    
    void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffSet, target.position.y + yOffSet, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
}
