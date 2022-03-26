using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forCamera : MonoBehaviour
{

    private void Awake()
    {
        Camera.main.orthographicSize = ((float)Screen.height / (float)Screen.width) * 3.5f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {

        Camera.main.orthographicSize = ((float)Screen.height / (float)Screen.width) * 3.5f;


    }


}
