using UnityEngine;
using System.Collections;

public class replay : MonoBehaviour 
{
	public AudioSource sorce;
	void OnMouseDown()
	{
		Debug.Log("Click");
		sorce.Play();
		StartCoroutine("loadLevel");
	}
	public IEnumerator loadLevel()
	{
		yield return new WaitForSeconds(0.5f);
		Application.LoadLevel("moive");
	}


}
