using UnityEngine;
using System.Collections;

public class MouseControl : MonoBehaviour {

    Walker walker;

	// Use this for initialization
	void Awake () {
        walker = GetComponent<Walker>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButton(0) && !walker.SpottedSomething) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground"))) {
                if (walker.IsLeader) {
                    walker.agent.SetDestination(new Vector3(hit.point.x, hit.point.y, hit.point.z));
                }
                else {
                    walker.agent.SetDestination(new Vector3(hit.point.x + walker.xOffset, hit.point.y, hit.point.z + walker.zOffset));
                }
            }
        }
	
	}
}
