using UnityEngine;
using System.Collections;
using System.Text;

public class TestNetState : MonoBehaviour
{
	private StringBuilder NetState = new StringBuilder();

	// Use this for initialization
	void Start()
	{
		//当网络不可用时
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			NetState = new StringBuilder("404");
		}
		//当用户使用WiFi时
		if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
		{
			NetState = new StringBuilder("Wifi");
			Debug.Log("Wifi");
		}
		//当用户使用移动网络时
		if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
		{
			NetState = new StringBuilder("Mobile NetWork");
			Debug.Log("Mobile NetWork");
		}
	}


	void OnGUI()
	{
		GUI.contentColor = Color.red;
		GUI.Label(new Rect(Screen.width/2-50,Screen.height/2-100,100,40),"网络连接:"+NetState);
	}

}
