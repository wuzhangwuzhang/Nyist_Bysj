using UnityEngine;

public class BackToMenue : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			Application .LoadLevel("first");
		}
	}

	void OnMOuseDown()
	{
		Debug.Log("back");
	}
}
