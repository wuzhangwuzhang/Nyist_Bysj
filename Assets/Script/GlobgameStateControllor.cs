using UnityEngine;
using System.Collections;

public class GlobgameStateControllor : MonoBehaviour 
{
  
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape))
		{
            Application.LoadLevel("first");
		}
		if(Input.GetKey(KeyCode.Home))
		{
			Application.Quit();
		}
	}
}
