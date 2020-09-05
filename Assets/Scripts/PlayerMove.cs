using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    private Vector3 movement = new Vector3();
    public float speed = 10f;

    public Camera cam;
    private Vector3 height;
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        height = cam.transform.position;
    }

    void Update(){
        movement = Vector3.zero;

        if(Input.GetKey("w")){
            movement.z = 1;
        } else {
            // movement.z = 0;
        }

        if(Input.GetKey("a")){
            movement.x = -1;
        } else {
            // movement.x = 0;
        }

        if(Input.GetKey("d")){
            movement.x = 1;
        } else {
            // movement.x = 0;
        }

        if(Input.GetKey("s")){
            movement.z = -1;
        } else {
            // movement.z = 0;
        }
        movement = movement*speed;

        if(movement != Vector3.zero){
            cam.transform.position = Vector3.Lerp(cam.transform.position,
            new Vector3(cam.transform.position.x, 5f+0.2f*(Mathf.Sin(5f*Time.time)), cam.transform.position.z), Time.deltaTime*100);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * Time.deltaTime);   
    }
}
