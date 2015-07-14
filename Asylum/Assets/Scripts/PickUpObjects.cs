using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PickUpObjects : MonoBehaviour {
	public int turnSpeed = 150;
	public float risingY = 3.5f;
	public Camera cam1;
	public Camera cam2;
	Vector3 oldScale;
	Vector3 oldCubeRotation;
	Vector3 oldCubePosition;
	GameObject colliders;
	Object tempcam;
	Vector3 camPos;
	Vector3 camRot; 
	Vector3 cubePos;
	Camera cam3;
	bool rising;
	bool dropping;

	bool doneRising;
	bool rotateAround;

	bool hasPlayer = false;
	bool beingCarried = false;

	void Start()
	{

		//this.GetComponent<Collider>().transform.localScale += new Vector3(3, 3, 3);

		colliders = new GameObject();
		colliders.AddComponent<SphereCollider>();
		colliders.GetComponent<SphereCollider>().isTrigger = true;
		colliders.transform.parent = transform;
		colliders.transform.localPosition = new Vector3(0, 0, 0);
		colliders.transform.localScale = new Vector3(10, 10, 10);
		colliders.name = "TriggerSphere";


		cam1 = GameObject.Find ("MainCamera").GetComponent<Camera>();
		if(GameObject.Find("Camera")==null)
		{
			//Debug.Log("Please create a second Camera GameObject");
			tempcam = Object.Instantiate(GameObject.Find ("MainCamera"));
			tempcam.name = "Camera";
			foreach(Transform child in GameObject.Find ("Camera").transform)
			{
				GameObject.Destroy(child.gameObject);
			}
			GameObject.Find ("Camera").GetComponent<Camera>().enabled = false;
			GameObject.Find ("Camera").GetComponent<AudioListener>().enabled = false;
			GameObject.Find ("Camera").transform.position = GameObject.Find ("MainCamera").transform.position - new Vector3(0, .2f, 0);
		}

		cam2 = GameObject.Find("Camera").GetComponent<Camera>();
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.tag!="Asylum")
			hasPlayer = true;
	}

	void OnTriggerExit(Collider other)
	{
		hasPlayer = false;
	}


	void Update()
	{
		if(beingCarried && GetComponent<Rigidbody>().velocity.y < 0)
		{
			if(GetComponent<Rigidbody>().isKinematic!=true)
				GetComponent<Rigidbody>().isKinematic = true;
			doneRising = true;
			dropping = true;
			rising = false;
		}
		if(beingCarried && doneRising)
		{
			//this is the cue to put it down
			if(Input.GetKeyDown ("e"))
			{
				//enable Main Camera (cam1) and disable Rotate Camera (cam2)
				cam1.enabled = true;
				cam2.enabled = false;

				//set the object back to kinematic
				GetComponent<Rigidbody>().isKinematic = false;


				//place the cube back in its old position
				transform.position = oldCubePosition;
				transform.rotation = Quaternion.Euler(oldCubeRotation);
				transform.localScale = oldScale;

				beingCarried = false;
				hasPlayer = false;
			
				if(GameObject.Find ("Player")==null)
				{
					Debug.Log("Please change the FirstPersonController's name to Player");
				}
				else
				{
					GameObject.Find ("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
					GameObject.Find ("Player").GetComponent<RigidbodyFirstPersonController>().enabled = true;
				}
				rotateAround = false;

				dropping = true; 
				rising = false;

				transform.parent = null;

			}
		}
		else
		{
			if(Input.GetKeyDown ("e")&& hasPlayer)
			{
				oldCubePosition = transform.position;
				oldCubeRotation = transform.rotation.eulerAngles;
				oldScale = transform.localScale;

				//change the 2nd perspective to the same as the Main Camera's perspective
				cam2.transform.position = GameObject.Find("Player").transform.position;
				cam2.transform.rotation = GameObject.Find ("MainCamera").transform.rotation;


				cubePos = transform.position;
				camPos = cam2.transform.position;
				camRot = cam2.transform.rotation.eulerAngles;

				//cubePos.y += .7f;	
			
				camRot.x -= 30;

				cam2.transform.eulerAngles = camRot;
				cam2.transform.position = camPos;
				//set the object back to kinematic
				GetComponent<Rigidbody>().isKinematic = false;
				GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(0, risingY, 0), transform.localPosition ,ForceMode.VelocityChange);

				rising = true;


				if(GetComponent<Rigidbody>().velocity.y < 0)
				{
					//print ("oi");


				}
					
				//enable the 2nd perspective and disable the first
				cam1.enabled = false;
				cam2.enabled = true;

				//set the object to rigid so you can lift it
				//if(transform.position.y >= .8)

				//set the 2nd perspective to rotate around the object
				rotateAround = true;

				//freeze the player in place
				GameObject.Find ("Player").GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;



				//GameObject.Find ("MainCamera").transform.parent = null;

				GameObject.Find ("Player").GetComponent<RigidbodyFirstPersonController>().enabled = false;

				beingCarried = true;
			}
		}
		if(rotateAround)
		{
			float xMove = Input.GetAxis("Mouse X");
			float yMove = Input.GetAxis ("Mouse Y");
			float scaleChange = Input.GetAxis("Mouse ScrollWheel")/5;

			if(transform.localScale.y < .4 && transform.localScale.y > .1)
			{
				if(transform.localScale.y + scaleChange < .4 && transform.localScale.y + scaleChange > .1)
					transform.localScale += new Vector3(scaleChange, scaleChange, scaleChange);
			}


			
			cam2.transform.RotateAround(transform.position, Vector3.up, (xMove*turnSpeed)*Time.deltaTime);
	
		}


		
	}
}