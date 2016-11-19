using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float smoothing = 5f;
    public Vector3 playerPos;

    private Vector3 offset;

    void Awake() {
        HumanHealth.OnHumanDied += HumanHealth_OnHumanDied;        
    }

    void HumanHealth_OnHumanDied(HumanHealth e) {
        target = GameObject.FindGameObjectWithTag("Follower").transform;
    }

    void Start() {        
        offset = transform.position - target.position;
    }

    void FixedUpdate() {        
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
    }
}
