using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    /*
     * Скорее всего, этот скрипт был призван бороться с "заикающимися" физическими движениями. Он двигает
     * визуальное тело игрока за физическим с отставанием.
     * 
     */

    public Transform target;
    public float smoothing = 5f;    

    private Vector3 offset;
    
    void Start() {        
        offset = transform.position - target.position;
    }

    void LateUpdate() {        
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        transform.rotation = target.transform.rotation;
    }
}
