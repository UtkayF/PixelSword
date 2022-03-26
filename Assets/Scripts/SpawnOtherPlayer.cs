using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnOtherPlayer : MonoBehaviour
{

    public GameObject SOPL;
    public Vector2 SpawnOtherPlayer_Location;

    public GameObject SHL;
    public Vector2 STOPHERE_Location;

    public GameObject[] spawnerOBJ;

    public GameObject obj;

    public float spawnTimers;
    public float spawnTimerPLUS;
    public float otherPlayerComingSpeed;
    public bool enemyExists = false;

    public ACH GMScript;
    public Button Go_BTN;
    public int randomNum;

    public int stageNumberForSpawnObject;

    void Start()
    {
        SpawnOtherPlayer_Location = SOPL.transform.position;
        STOPHERE_Location = SHL.transform.position;
        stageNumberForSpawnObject = GMScript.Stage;
    }

    // Update is called once per frame
    void Update()
    {
        stageNumberForSpawnObject = GMScript.Stage;

        if (GMScript.NewEnemiesWanted)
        {
            GMScript.IncomingEnemy = Random.Range(GMScript.RandomX, GMScript.RandomY);
            GMScript.NewEnemiesWanted = false;
        }


        if (GMScript.IncomingEnemy != 0)
        {
            if (!enemyExists)
            {
                print("Spawner UZUNLUĞU ->" + spawnerOBJ.Length);

                /*
                if (GMScript.EMH == 1) { 
                    randomNum = Random.Range(0, spawnerOBJ.Length);
                }

                if (GMScript.EMH == 2)
                {
                    randomNum = Random.Range(0, spawnerOBJ.Length);
                }

                if (GMScript.EMH == 3)
                {
                    randomNum = Random.Range(0, spawnerOBJ.Length);
                }

                if (GMScript.EMH == 4)
                {
                    randomNum = Random.Range(0, spawnerOBJ.Length);
                }
                */
                randomNum = Random.Range(0, spawnerOBJ.Length);
                print(randomNum);
                obj = Instantiate(spawnerOBJ[randomNum], SpawnOtherPlayer_Location, Quaternion.Euler(0, 0, 0));
                obj.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 0);
                GMScript.IncomingEnemy = GMScript.IncomingEnemy - 1;
                enemyExists = true;
            }
        }
        else{
            if (enemyExists == false)
            {
                GMScript.stageButtonClose = true;
            }
        }
        

    }
}
