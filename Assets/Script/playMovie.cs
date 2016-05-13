using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		Handheld.PlayFullScreenMovie("test.mp4", Color.black, FullScreenMovieControlMode.Minimal);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKey(KeyCode.Escape))
		{
			Application.LoadLevel("menue");
		}
	}

}
