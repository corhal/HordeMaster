using UnityEngine;
using System.Collections;

public class InGameButton : MonoBehaviour {

    public GameObject ObjectToToggle;
    ITogglable togglable;

    void Awake() {
        togglable = ObjectToToggle.GetComponent<ITogglable>();
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Human" && Input.GetKeyDown("f")) {
            //Debug.Log(ObjectToToggle + "; " + this.gameObject);
            togglable.Toggle();
        }
    }
}
