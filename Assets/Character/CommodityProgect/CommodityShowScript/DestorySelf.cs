using UnityEngine;
using System.Collections;

public class DestorySelf : MonoBehaviour {
	
	public float destoryTime = 2;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		Destroy(this.gameObject,destoryTime);
	}
}
