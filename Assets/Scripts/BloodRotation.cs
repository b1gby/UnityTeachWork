using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodRotation : MonoBehaviour
{
    public Transform cameraTransform;
    public float blood;
    public float loss;

    public float scaleZ;

    private bool isMovePos = false;

    // Start is called before the first frame update
    void Start()
    {
        scaleZ = transform.localScale.z;
        cameraTransform = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        isMovePos = transform.parent.GetComponent<Health>().isDamage;
        blood = transform.parent.GetComponent<Health>().getHealth();
        loss = transform.parent.GetComponent<Health>().getLoss();

        //血条跟随相机移动
        this.transform.localRotation = Quaternion.Euler(new Vector3(transform.rotation.eulerAngles.x,
            cameraTransform.rotation.eulerAngles.y + 90, transform.rotation.eulerAngles.z));

        this.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
            blood / 100 * scaleZ);

        if(isMovePos)
        {
            this.transform.position -= this.transform.forward * loss / 100 * scaleZ / 2;
            isMovePos = false;
        }
        
    }
}
