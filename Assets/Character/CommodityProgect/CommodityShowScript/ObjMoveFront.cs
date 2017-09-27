using UnityEngine;
using System.Collections;

public class ObjMoveFront : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}


	public float MoveSpeed = 2f;
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward*MoveSpeed*Time.deltaTime);
	}
}
