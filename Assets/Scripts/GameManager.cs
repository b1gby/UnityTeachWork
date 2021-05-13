using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject GameStartTip;
    // Start is called before the first frame update
    void Start()
    {
        GameStartTip.GetComponent<TxtTimer>().txtTimer("Game Start!", 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
