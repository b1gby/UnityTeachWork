using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TxtTimer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void txtTimer(string str, int time)
    {
        GetComponent<Text>().text = str;
        Invoke("NotDisplayTxt", time);
    }

    void NotDisplayTxt()
    {
        GetComponent<Text>().text = "";
    }
}
