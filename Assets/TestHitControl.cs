using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class TestHitControl : MonoBehaviour {

	public GameObject obj;
	private UnityARSessionNativeInterface m_session;

	// Use this for initialization
	void Start () {
		m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface ();
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0) {
			if (Input.GetTouch (0).phase == TouchPhase.Began) {
				createGameObject ();
			
			}
		
		}
		
	}

	void createGameObject(){
		Vector3 screenPos = Camera.main.ScreenToViewportPoint (Input.GetTouch (0).position);
		ARPoint point = new ARPoint{ x= screenPos.x,y=screenPos.y };

		ARHitTestResultType[] resultTypes = {
			ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
			// if you want to use infinite planes use this:
			//ARHitTestResultType.ARHitTestResultTypeExistingPlane,
			ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
			ARHitTestResultType.ARHitTestResultTypeFeaturePoint
		}; 

		foreach (ARHitTestResultType resultType in resultTypes)
		{

			List<ARHitTestResult> hitResults = m_session.HitTest (point, resultType);
			if (hitResults.Count > 0) {
				GameObject currentObj = GameObject.Instantiate (obj);
				currentObj.transform.position = UnityARMatrixOps.GetPosition (hitResults [0].worldTransform);
				currentObj.transform.rotation = UnityARMatrixOps.GetRotation (hitResults [0].worldTransform);
				currentObj.transform.LookAt (new Vector3(Camera.main.transform.position.x, currentObj.transform.position.y, Camera.main.transform.position.z));
				

			}
		}
	
	}
}
