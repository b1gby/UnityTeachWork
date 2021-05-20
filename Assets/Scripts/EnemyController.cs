using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    // 向随机方向（除y以外）移动随机距离，随机距离控制在（0，5）
    Vector3 current_direction;
    public float speed = 30f;
    public float v_lerp = 10f;

    // Start is called before the first frame update
    void Start()
    {
        //Slider slider;
        //AudioSource audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        //audioSource.volume = slider.value;
    }

    // Update is called once per frame
    void Update()
    {
        moveRandom();
    }

    void moveRandom()
    {
        // 随机方向
        float ranDirX = (float)Random.Range(-100, 100) / 100;
        float ranDirZ = (float)Random.Range(-100, 100) / 100;

        Vector3 dir = new Vector3(ranDirX, 0, 0) + new Vector3(0, 0, ranDirZ);
        Debug.Log(dir);

        if (dir != Vector3.zero)
        {
            // 方向的转变过程
            current_direction = Vector3.Lerp(current_direction, dir, Time.deltaTime * v_lerp);

            transform.position += current_direction * speed * Time.deltaTime;
        }
    }
}
