using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject button;

    Text txt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pressButton()
    {
        txt = button.GetComponentInChildren<Text>();
        txt.text = "Pressed";
    }

    public void InputFieldValueChange()
    {
        Debug.Log(GameObject.Find("InputField").GetComponentInChildren<Text>().text);
    }

    public void gameStart()
    {
        int num = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(num+1);
    }
}
