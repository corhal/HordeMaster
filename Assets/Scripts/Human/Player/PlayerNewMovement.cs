using UnityEngine;
using System.Collections;

public class PlayerNewMovement : MonoBehaviour {

    public float Speed = 6f;
    public GameObject MyHuman;

    Vector3 movement;
    
    //Animator animator;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    public delegate void OnCollidedWithSomethingEvent(GameObject e);
    public static event OnCollidedWithSomethingEvent OnCollidedWithSomething;

    void Awake() {      
        floorMask = LayerMask.GetMask("Ground");
        //animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        //myReloader = GetComponentInChildren<HumanGunReloader>();
    }

    Vector3 newPosition;

    void Update() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        movement.Set(h, 0f, v);
        movement = movement.normalized * Speed * Time.fixedDeltaTime;

        newPosition = GetComponent<Rigidbody>().position + movement;
    }

    void FixedUpdate() {
        playerRigidbody.velocity = Vector3.zero;
        playerRigidbody.AddForce(transform.forward);
        Turning();        
        //Animating(h, v);
    }

    void Turning() {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {
            Vector3 playerToMouse = floorHit.point - transform.position;
            playerToMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
            playerRigidbody.MoveRotation(newRotation);
        }
    }

    void OnTriggerEnter(Collider other) {
        OnCollidedWithSomething(other.gameObject);
    }


    /*void Animating(float h, float v) {
        bool walking = h != 0f || v != 0f;
        animator.SetBool("IsWalking", walking);
    }*/
}
