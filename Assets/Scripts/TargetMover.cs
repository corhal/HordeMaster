using UnityEngine;
using System.Collections;

public class TargetMover : MonoBehaviour {

    public float speed;
    public GameObject WalkerSample;
    public Transform walkerTransform;
    public NavMeshAgent agent;

	// Use this for initialization
	void Start () {
        agent = WalkerSample.GetComponent<NavMeshAgent>();
        walkerTransform = WalkerSample.GetComponent<Transform>();
        //speed = 0.1f;
	}
	
	// Update is called once per frame
	void Update () { // надо научить их ходить по диагонали
        if (Input.GetKey(KeyCode.W)) {            
            if (Input.GetKey(KeyCode.D)) {                
                transform.position = new Vector3(walkerTransform.position.x + speed, walkerTransform.position.y, walkerTransform.position.z + speed);
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.position = new Vector3(walkerTransform.position.x - speed, walkerTransform.position.y, walkerTransform.position.z + speed);
            }
            else {
                transform.position = new Vector3(walkerTransform.position.x, walkerTransform.position.y, walkerTransform.position.z + speed);
            }            
        }
        if (Input.GetKey(KeyCode.S)) {
            if (Input.GetKey(KeyCode.D)) {
                transform.position = new Vector3(walkerTransform.position.x + speed, walkerTransform.position.y, walkerTransform.position.z - speed);
            }
            if (Input.GetKey(KeyCode.A)) {
                transform.position = new Vector3(walkerTransform.position.x - speed, walkerTransform.position.y, walkerTransform.position.z - speed);
            }
            else {
                transform.position = new Vector3(walkerTransform.position.x, walkerTransform.position.y, walkerTransform.position.z - speed);
            }            
        }
        if (Input.GetKey(KeyCode.A)) {
            if (Input.GetKey(KeyCode.W)) {
                transform.position = new Vector3(walkerTransform.position.x - speed, walkerTransform.position.y, walkerTransform.position.z + speed);
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.position = new Vector3(walkerTransform.position.x - speed, walkerTransform.position.y, walkerTransform.position.z - speed);
            }
            else {
                transform.position = new Vector3(walkerTransform.position.x - speed, walkerTransform.position.y, walkerTransform.position.z);
            }
        }
        if (Input.GetKey(KeyCode.D)) {
            if (Input.GetKey(KeyCode.W)) {
                transform.position = new Vector3(walkerTransform.position.x + speed, walkerTransform.position.y, walkerTransform.position.z + speed);
            }
            if (Input.GetKey(KeyCode.S)) {
                transform.position = new Vector3(walkerTransform.position.x + speed, walkerTransform.position.y, walkerTransform.position.z - speed);
            }
            else {
                transform.position = new Vector3(walkerTransform.position.x + speed, walkerTransform.position.y, walkerTransform.position.z);
            }
            
        }        
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            transform.position = WalkerSample.transform.position;
        }
	}
}
