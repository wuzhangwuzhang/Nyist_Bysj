using UnityEngine;
using System.Collections;


public class inAndOut : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	public GameObject helpPanel;
	public GameObject inPos;
	public GameObject outPos;
	bool isIn = false;
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			if(!isIn)
			{
				isIn = true;
				iTween.MoveTo(helpPanel,inPos.transform.position,2f);
				Debug.Log("Comeing in");
			}
			else
			{
				isIn = false;
				iTween.MoveTo(helpPanel,outPos.transform.position,2f);
				Debug.Log("Comeing out");				
			}
		}
	}

}
