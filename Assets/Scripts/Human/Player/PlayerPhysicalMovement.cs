using UnityEngine;
using System.Collections;

public class PlayerPhysicalMovement : MonoBehaviour {

	/*
	 * Видимо, этот скрипт двигает физическое тело игрока. 
	 */

    public float Speed;
    public float StepSoundDistance;
    public GameObject MyHuman;

    Vector3 movement;
    
    //Animator animator;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100f;

    public delegate void OnCollidedWithSomethingEvent(GameObject e);
    public static event OnCollidedWithSomethingEvent OnCollidedWithSomething;

    public delegate void OnHumanStepSoundEvent(Vector3 position, float MaxSoundDistance);
    public static event OnHumanStepSoundEvent OnHumanStepSound;

    void Awake() {      
        floorMask = LayerMask.GetMask("Ground");
        //animator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        //myReloader = GetComponentInChildren<HumanGunReloader>();
    }

    void FixedUpdate() {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

		Move(h, v);
        Turning();        
        //Animating(h, v);
    }

    void Move(float h, float v) {
        movement.Set(h, 0f, v);

        movement = movement.normalized * Speed * Time.deltaTime;
        playerRigidbody.MovePosition(GetComponent<Rigidbody>().position + movement);
        if (h != 0 || v != 0) {
            OnHumanStepSound(transform.position, StepSoundDistance);
        }        
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
