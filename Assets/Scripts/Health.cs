using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private float health = 100;

    private float loss;

    public bool isDamage = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
        {
            Destroy(gameObject);
        }
        isDamage = false;
    }

    public void healthDamage(float damage)
    {
        this.health -= damage;
        this.loss = damage;
        this.isDamage = true;
        GameObject.Find("Slider").GetComponentInChildren<Slider>().value = health/100;
    }

    private void OnCollisionEnter(Collision collision)
    {
    }

    public float getHealth()
    {
        return health;
    }

    public float getLoss()
    {
        return loss;
    }
}
