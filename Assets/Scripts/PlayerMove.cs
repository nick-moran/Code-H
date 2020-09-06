using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public bool bobbing = true;
    public Rigidbody rb;
    public float speed = 10f;
    public Camera cam;
    private Vector3 movement = new Vector3();
    private Vector3 height;
    
    public float dX = 2f;
    public float sensitivity = 10f;
    public float cycleAmt = 10f;
    public float amplitude = 0.1f;
    public float distFromPlayer = 2.29f;
    public float crouchSpeed = 5f;
    private Vector3 rotateVector = new Vector3();
    private float translateX;
    
    //WILL BE DELETED ONCE WE HAVE A CROUCH ANIMATION
    private Vector3 playerScale = new Vector3();
    private Vector3 modifiedPlayerScale = new Vector3();
    private float timeStart = 0.0f;

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
        translateX = Input.GetAxis("Mouse X") * dX;
        rotateVector.Set(0, translateX, 0);
        rotateVector*=sensitivity;
        transform.Rotate(rotateVector);

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

        movement = transform.rotation * move() * activeSpeed;

        if(bobbing && movement != Vector3.zero){
            cam.transform.position = Vector3.Lerp(cam.transform.position,
            new Vector3(cam.transform.position.x, 
                transform.position.y+distFromPlayer+amplitude*(Mathf.Sin(cycleAmt*Time.time)), 
                cam.transform.position.z), Time.deltaTime*100);
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
