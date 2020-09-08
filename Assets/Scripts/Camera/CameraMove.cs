using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    // Start is called before the first frame update
    public float sensitivity = 4f;

    private float translateY;
    private float translateX;
    public Transform target;

    public Vector3 offset = new Vector3();
    
    void Start(){
        offset = transform.position - target.position;
    }

    void Update()
    {
        translateY = Input.GetAxis("Mouse Y");
        translateX = Input.GetAxis("Mouse X");

        transform.RotateAround(target.position, Vector3.up, translateX * sensitivity);
        transform.LookAt(target);

        // targetPosition.z-=6.09f;

        transform.position = target.position + offset;
    }
}
