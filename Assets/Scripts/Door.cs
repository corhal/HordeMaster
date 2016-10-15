using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour, ITogglable {

    public bool StartsOpen;
    public bool ClosesAutomatically;
    bool isOpen;
    bool isMoving;
    public float smoothing = 5f;
    float timer;
    public float TimeBetweenOpenClose;
    Vector3 targetPos;
    Vector3 openPosition;
    Vector3 closedPosition;

    public GameObject DoorObject;

    public string AxisToSlide; // "x" or "z"
    public bool PositiveDirection; // where to slide

    void Awake() {        
        if (AxisToSlide == "z") {
            if (PositiveDirection) {
                openPosition = new Vector3(transform.position.x, transform.position.y, (transform.position.z + transform.localScale.z));
            }
            else {
                openPosition = new Vector3(transform.position.x, transform.position.y, (transform.position.z - transform.localScale.z));
            }            
        }
        if (AxisToSlide == "x") {
            if (PositiveDirection) {
                openPosition = new Vector3((transform.position.x + transform.localScale.z), transform.position.y, transform.position.z);
            }
            else {
                openPosition = new Vector3((transform.position.x - transform.localScale.z), transform.position.y, transform.position.z);
            }
        }
        
        closedPosition = transform.position;
        if (StartsOpen) {
            Open(); // сейчас дверь всегда закрывается, если мимо нее пройти
        }
    }

    /*void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Human") {
            Open();
        }
    }*/

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Human" && ClosesAutomatically) {
            StartCoroutine(WaitClose());
        }
    }

    void Open() {
        targetPos = openPosition;
        isMoving = true;
    }

    void Close() {
        targetPos = closedPosition;
        isMoving = true;
    }

    IEnumerator WaitClose() {
        yield return new WaitForSeconds(TimeBetweenOpenClose);
        targetPos = closedPosition;
        isMoving = true;
    }    

    void Update() {
        timer += Time.deltaTime;
        if (isMoving) {
            DoorObject.transform.position = Vector3.Lerp(DoorObject.transform.position, targetPos, smoothing * Time.deltaTime);
            if (Mathf.Approximately(DoorObject.transform.position.magnitude, targetPos.magnitude)) {
                isMoving = false;
                isOpen = !isOpen;
            }
        }
    }

    public void Toggle() {
        if (!isOpen) {
            Open();
        }
        else {
            Close();
        }
    }
}
