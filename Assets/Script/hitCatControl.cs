using System;
using System.Collections.Generic;

namespace UnityEngine.XR.iOS
{
	public class hitCatControl : MonoBehaviour
	{
		public Transform m_HitTransform;

		private float currScale;
		private float scaleMax = 5f;
		private float scaleMin = 1f;

		private Animator anim;
		private Rigidbody rb;
		private int speedHash = Animator.StringToHash("speed");

		public GameObject testObj;


		public GameObject shotPrefab;

		void Awake() {
			Joystick.JoystickMoved += UpdateMove;
		}

		void OnDestroy() {
			Joystick.JoystickMoved -= UpdateMove;
		}

		// Use this for initialization
		void Start () {
			currScale = Mathf.Clamp (transform.localScale.x, scaleMin, scaleMax);
			anim = gameObject.GetComponent<Animator> ();
			rb = gameObject.GetComponent<Rigidbody> ();
		}

		bool HitTestWithResultType (ARPoint point, ARHitTestResultType resultTypes)
		{
			List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (point, resultTypes);
			if (hitResults.Count > 0) {
				foreach (var hitResult in hitResults) {
					Debug.Log ("Got hit!");
					m_HitTransform.position = UnityARMatrixOps.GetPosition (hitResult.worldTransform);
					m_HitTransform.rotation = UnityARMatrixOps.GetRotation (hitResult.worldTransform);
					Debug.Log (string.Format ("x:{0:0.######} y:{1:0.######} z:{2:0.######}", m_HitTransform.position.x, m_HitTransform.position.y, m_HitTransform.position.z));
					return true;
				}
			}
			return false;
		}

		// Update is called once per frame
		void Update () {
//			if (Input.touchCount > 0 && m_HitTransform != null)
//			{
//				var touch = Input.GetTouch(0);
//				if (touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved)
//				{
//					
//				}
//			}
		}

		public void SetPosition() {
			// Project from the middle of the screen to look for a hit point on the detected surfaces.
			var screenPosition = Camera.main.ScreenToViewportPoint (new Vector2 (Screen.width / 2f, Screen.height / 2f));

			//var screenPosition = Camera.main.ScreenToViewportPoint(touch.position);
			ARPoint point = new ARPoint {
				x = screenPosition.x,
				y = screenPosition.y
			};

			// prioritize reults types
			ARHitTestResultType[] resultTypes = {
				ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent, 
				// if you want to use infinite planes use this:
				//ARHitTestResultType.ARHitTestResultTypeExistingPlane,
				ARHitTestResultType.ARHitTestResultTypeHorizontalPlane, 
				ARHitTestResultType.ARHitTestResultTypeFeaturePoint
			}; 

			foreach (ARHitTestResultType resultType in resultTypes)
			{
				if (HitTestWithResultType (point, resultType))
				{
					return;
				}
			}

//			ARPoint pt = new ARPoint {
//				x = screenPosition.x,
//				y = screenPosition.y
//			};
//
//			// Try to hit within the bounds of an existing AR plane.
//			List<ARHitTestResult> hitResults = UnityARSessionNativeInterface.GetARSessionNativeInterface ().HitTest (
//				pt, 
//				ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent);
//
//			if (hitResults.Count > 0) { // If a hit is found, set the position and reset the rotation.
//				transform.rotation = Quaternion.Euler (Vector3.zero);
//				transform.position = UnityARMatrixOps.GetPosition (hitResults[0].worldTransform);
//			}
		}

		public void Jump() {
			rb.AddForce (Vector3.up * 80f);
		}

		public void LookAt(){
			transform.LookAt (Camera.main.transform.position);
			transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);

			//testObj.SetActive (!testObj.activeSelf);
			testObj.SetActive(true);
		}

		public void Shoot(){

			GameObject go = Instantiate (shotPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
			//go.transform.GetComponent<Rigidbody> ().AddForce (go.transform.forward * 5);

		}

		public void UpScale() {
			if (Mathf.Approximately(currScale,scaleMax)) return;
			currScale += 1f;
			transform.localScale = new Vector3 (currScale, currScale, currScale);
		}

		public void DownScale() {
			if (Mathf.Approximately(currScale,scaleMin)) return;
			currScale -= 1f;
			transform.localScale = new Vector3 (currScale, currScale, currScale);
		}

		private void UpdateMove (Vector2 input) {
			if (input.Equals (Vector2.zero)) {
				anim.SetFloat (speedHash, 0f);
				LookAt ();
				return;
			}
			testObj.SetActive(false);

			Vector3 inputAxes = new Vector3 (input.x, 0, input.y);
			anim.SetFloat (speedHash, inputAxes.magnitude); // Update the animator parameter for speed based on the joystick.
			SetLookDirection (inputAxes); // Set the cat to look in the correct direction

			// Move the cat, the animator will handle triggering the correct animations.
			transform.localPosition += (transform.forward * inputAxes.magnitude * Time.deltaTime);
		}

		void SetLookDirection(Vector3 inputAxes) {
			// Get the camera's y rotation, then rotate inputAxes by the rotation to get up/down/left/right according to the camera
			Quaternion yRotation = Quaternion.Euler (0, Camera.main.transform.rotation.eulerAngles.y, 0);
			Vector3 lookDirection = (yRotation * inputAxes).normalized;
			transform.rotation = Quaternion.LookRotation (lookDirection);
		}

		public void buttonTest(){
			testObj.SetActive (true);

		}



	}
}
