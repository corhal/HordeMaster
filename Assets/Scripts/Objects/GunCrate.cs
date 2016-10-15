using UnityEngine;
using System.Collections;

public class GunCrate : MonoBehaviour {

    public string GunType;
    public GameObject GunPrefab;
    public GameObject RiflePrefab;
    public GameObject ShotgunPrefab;
    public GameObject StoredGun;

	// Use this for initialization
	void Start () {
        if (StoredGun == null) {
            float coinToss = Random.Range(0, 1f);
            Debug.Log(coinToss);
            if (coinToss < 0.33f) {
                StoredGun = GunPrefab;
            }
            if (coinToss >= 0.33f && coinToss < 0.66f) {
                StoredGun = RiflePrefab;
            }
            if (coinToss >= 0.66f && coinToss < 1f) {
                StoredGun = ShotgunPrefab;
            }
        }        
	}	
}
