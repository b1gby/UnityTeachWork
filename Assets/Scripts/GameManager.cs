using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameStartTip;
    public GameObject EnemyPrefab;
    public GameObject Enemys;

    private int numEnemy = 10;
    // Start is called before the first frame update
    void Start()
    {
        init();

        GameStartTip.GetComponent<TxtTimer>().txtTimer("Game Start!", 3);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }


    }

    void init()
    {
        for(int i = 0; i < numEnemy;i++)
        {
            float RandomX = Random.Range(0, 100);
            float RandomZ = Random.Range(0, 100);
            Instantiate(EnemyPrefab, new Vector3(RandomX,0,RandomZ), Quaternion.identity,Enemys.transform);
        }
    }
}
