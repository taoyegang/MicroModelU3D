﻿using UnityEngine;
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
			//案例展示
			Debug.Log("uploading");
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			jo.Call("StartUploading", "hello uploading");
			break;
		}
		case "ButtonShow":
		{
			//案例展示
			Debug.Log("example");
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			jo.Call("StartExample", "hello show");
			break;
		}
		case "ButtonScan":
		{
			//扫一扫
			Debug.Log("scan");
			AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
			AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
			jo.Call("StartScanner", "hello scanner");

			break;
		}
		}
	}
}
