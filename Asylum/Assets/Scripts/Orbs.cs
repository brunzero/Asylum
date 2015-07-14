using UnityEngine;
using System.Collections;

public class Orbs : MonoBehaviour {
	public int numOrbs = 3;
	public GameObject sphere;
	public float orbitDistance = 1f;
	GameObject[] sphereArray;
	GameObject player;

	// Use this for initialization
	void Start () {

		player = GameObject.Find("Player");
		sphereArray = new GameObject[numOrbs];

		for(int i = 0; i<numOrbs; i++)
		{
			sphereArray[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphereArray[i].transform.position = player.transform.position + new Vector3(0, i/numOrbs, 0);
			sphereArray[i].transform.localScale  = new Vector3(.1f, .1f, .1f);
			sphereArray[i].transform.parent = player.transform;
		}

	}
	
	// Update is called once per frame
	void LateUpdate () 
	{
		Vector3[] vectors;
		vectors = new Vector3[6];

		vectors[0] = Vector3.up;
		vectors[1] = Vector3.forward;
		vectors[2] = Vector3.right;

		//print (Mathf.CeilToInt(Random.Range (0f, 3f)));
		for(int i = 0; i<numOrbs; i++)
		{
			sphereArray[i].transform.position = player.transform.position + (sphereArray[i].transform.position - player.transform.position).normalized * orbitDistance;

			sphereArray[i].transform.RotateAround(player.transform.position, vectors[0], 50*Time.deltaTime);
			//sphereArray[1].transform.RotateAround(player.transform.position, vectors [0], 50*Time.deltaTime);
			//sphereArray[2].transform.RotateAround(player.transform.position, vectors[0], 50*Time.deltaTime);
		}

		
	}
}
