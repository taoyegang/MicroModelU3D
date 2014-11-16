using UnityEngine;
using System.Collections;

public class MainUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonClick()
	{
		Debug.Log ("name = " + UIButton.current.name);
		switch (UIButton.current.name) 
		{
		case "ButtonPlay":
		{
			Application.LoadLevel(2);
			break;
		}
		case "ButtonAbout":
		{

			break;
		}
		case "ButtonExit":
		{
			Application.Quit();
			break;
		}
		case "ButtonUploading":
		{
			//灾情上传
			break;
		}
		case "ButtonShow":
		{
			//案例展示
			break;
		}
		case "ButtonScan":
		{
			//扫一扫
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			if(jo != null) {
				jo.Call("StartScanner","调用二维码扫描");
			}else {
				Debug.Log("jo is null");
			}
			break;
		}
		}
	}
}
