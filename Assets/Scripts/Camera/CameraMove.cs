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
    public Vector3 rotate = new Vector3();
    
    void Start(){
        offset = transform.position - target.position;
    }

    void Update()
    {
        translateY += Input.GetAxis("Mouse Y");
        translateX -= Input.GetAxis("Mouse X");

        Vector3 tmp = new Vector3();
        tmp.x = (Mathf.Cos(translateX * (Mathf.PI / 180)) * offset.z ) + target.position.x;
        tmp.z = (Mathf.Sin(translateX * (Mathf.PI / 180)) * offset.z ) + target.position.z;
        //  tmp.y = Mathf.Sin(AngleV * (Mathf.PI / 180)) * CurrentDist + Target.position.y;
        
        tmp.y = offset.y + target.position.y;
        // tmp += offset;
        transform.position = Vector3.Slerp(transform.position, tmp, sensitivity);
        // rotate = new Vector3()
        // transform.LookAt(target, Vector3.up);
        Vector3 relativePos = target.position - transform.position;

        Quaternion lookRot = Quaternion.LookRotation(relativePos);
        lookRot.x = 0;
        lookRot.z = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, sensitivity);
        transform.Rotate(new Vector3(22, 0, 0));
    }
}
