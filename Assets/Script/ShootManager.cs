using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootManager : MonoBehaviour {

	public GameObject shotPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Shoot(){

		GameObject go = Instantiate (shotPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
		//go.transform.GetComponent<Rigidbody> ().AddForce (go.transform.forward * 5);

	}
}
