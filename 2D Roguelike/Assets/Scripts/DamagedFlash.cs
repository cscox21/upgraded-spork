using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagedFlash : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        gameObject.SetActive(false);
        other.gameObject.GetComponent<HurtPlayer>().HurtPlayer(damageToGive);
    }
}
