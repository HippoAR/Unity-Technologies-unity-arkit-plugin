using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.iOS;

public class sessionControl : MonoBehaviour {

	private UnityARSessionNativeInterface m_session;

	// Use this for initialization
	void Start () {
		m_session = UnityARSessionNativeInterface.GetARSessionNativeInterface ();
		
	}

	void OnGUI(){
		if (GUI.Button (new Rect (100, 100, 200, 50), "session pause")) {
			m_session.Pause ();
		}

		if (GUI.Button (new Rect (300, 100, 200, 50), "session run")) {
			m_session.Run ();
		}

		if (GUI.Button (new Rect (100, 200, 200, 50), "renew & run")) {
			ARKitWorldTrackingSessionConfiguration config = new ARKitWorldTrackingSessionConfiguration ();
			config.alignment = UnityARAlignment.UnityARAlignmentGravityAndHeading;
			config.planeDetection = UnityARPlaneDetection.Horizontal;
			config.enableLightEstimation = true;
			config.getPointCloudData = true;

			//m_session.RunWithConfig (config);
			m_session.RunWithConfigAndOptions(config,UnityARSessionRunOption.ARSessionRunOptionResetTracking);
		}

		if (GUI.Button (new Rect (300, 200, 200, 50), "renew & close")) {
			ARKitWorldTrackingSessionConfiguration config = new ARKitWorldTrackingSessionConfiguration ();
			config.alignment = UnityARAlignment.UnityARAlignmentGravity;
			config.planeDetection = UnityARPlaneDetection.Horizontal;
			config.enableLightEstimation = true;
			config.getPointCloudData = true;

			//m_session.RunWithConfig (config);
			m_session.RunWithConfigAndOptions(config,UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors);
		}



	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
