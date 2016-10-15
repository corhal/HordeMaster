using UnityEngine;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

    public GameObject WalkerPrefab;

    void Awake() {
        HumanHealth.OnHumanDied += Human_OnHumanDied;
    }

    void Human_OnHumanDied(HumanHealth e) {
        if (e.Infected) {
            StartCoroutine(Spawn(e.transform.position));
        }        
    }

    public void SpawnWalker(Vector3 position) {
        StartCoroutine(Spawn(position));
    }

    IEnumerator Spawn(Vector3 position) {        
        yield return new WaitForSeconds(0.1f);
        GameObject.Instantiate(WalkerPrefab, position, WalkerPrefab.transform.rotation);
    }    
}
