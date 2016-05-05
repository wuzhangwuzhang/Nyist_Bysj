using UnityEngine;
using System.Collections;

public class back : MonoBehaviour 
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
		yield return new WaitForSeconds(0.2f);
		Application.LoadLevel("first");
	}

}
