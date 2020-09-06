using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float dY = 2f;

    public float sensitivity = 0.5f;

    private float translateY;
    private Vector3 rotateVector = new Vector3();

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        translateY = Input.GetAxis("Mouse Y") * dY;

        rotateVector.Set(translateY, 0, 0);
        rotateVector*=sensitivity * -1;

        if(transform.eulerAngles.x+rotateVector.x > 1f && transform.eulerAngles.x+rotateVector.x < 34f){
            transform.Rotate(rotateVector);
        }
    }
}
