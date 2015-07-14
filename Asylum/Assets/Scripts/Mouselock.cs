using UnityEngine;
using System.Collections;

public class Mouselock : MonoBehaviour {


	void Update()
	{
		if(Input.GetKeyDown("escape"))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}
		if(Input.GetMouseButtonDown(0))
		{
			Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.lockState = CursorLockMode.Confined;
		}
	}

}