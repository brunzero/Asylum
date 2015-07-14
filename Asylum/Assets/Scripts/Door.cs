using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public bool open;
	public bool close;
	public int swingVelocity = 1;
	private Vector3 doorRotation = new Vector3(0, 90, 0);
	private float elapsedTime;
	private float yAngle;
	private float rotated;


	

	// Update is called once per frame
	void Update () {

		//set up the time standard. the shorter the elapsed time before every rotation, the
		//smoother it looks. i chose .01 seconds here.
		doorRotation.y = swingVelocity;
		yAngle = transform.rotation.eulerAngles.y;
		elapsedTime += Time.deltaTime;
		if(elapsedTime >= .001)
		{	
			if(open == true )
			{
				if(rotated < 90)
				{
					transform.Rotate (doorRotation);
					rotated += swingVelocity;
				}
				else open = false;
			}
			if(close == true)
			{
				if(rotated > 0)
				{
					transform.Rotate (-doorRotation);
					rotated -= swingVelocity;
				}
				else
					close = false;
			}

			elapsedTime = 0;
		}


	}
}
