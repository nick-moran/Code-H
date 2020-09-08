using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool bobbing = true;
    public Rigidbody rb;
    public float speed = 10f;
    public Transform cam;
    private Vector3 movement = new Vector3();
    private Vector3 height;
    
    public float sensitivity = 10f;
    public float cycleAmt = 10f;
    public float amplitude = 0.1f;
    public float distFromPlayer = 2.29f;
    public float crouchSpeed = 5f;
    
    //WILL BE DELETED ONCE WE HAVE A CROUCH ANIMATION
    private Vector3 playerScale = new Vector3();
    private Vector3 modifiedPlayerScale = new Vector3();
    private float translateX;
    private PlayerStateManager pM;
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        height = cam.transform.position;
        rb.position = Vector3.zero;

        playerScale = transform.localScale;
        modifiedPlayerScale = transform.localScale - new Vector3(0,0.5f,0);

        pM = new PlayerStateManager();
    }

    void Update(){



        if(Input.GetKey("left shift")){
            pM.isCrouching();
        } else {
            pM.isNotCrouching();
        }
        
        if(Input.GetKeyDown("left ctrl")){
            rb.AddForce(transform.forward * 500);
            pM.isDiving(Time.time,2);
        }

        (bool crouched, bool diving) = pM.activeSate(Time.time);

        float activeSpeed = speed;

        if(crouched || diving){
            transform.localScale = modifiedPlayerScale;
            activeSpeed = crouchSpeed;
        } else {
            transform.localScale = playerScale;
        }

        Quaternion camRot = cam.rotation;
        camRot.x = 0;
        camRot.z = 0;
        movement = transform.rotation * move() * activeSpeed;

        if(movement != Vector3.zero){
            transform.rotation = Quaternion.Slerp(transform.rotation, camRot, sensitivity * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(transform.position + movement * Time.deltaTime);   
    }

    Vector3 move(){
        Vector3 movement = Vector3.zero;

        if(Input.GetKey("w")){
            movement.z = 1;
        }

        if(Input.GetKey("a")){
            movement.x = -1;
        }

        if(Input.GetKey("d")){
            movement.x = 1;
        }

        if(Input.GetKey("s")){
            movement.z = -1;
        }

        return Vector3.Normalize(movement);
    }
}
