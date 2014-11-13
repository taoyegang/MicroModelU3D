using UnityEngine;
using System.Collections;

public class SelectSupportPanel : MonoBehaviour {

	SelectTypePanel _typePanel;

	// Use this for initialization
	void Start () {
		_typePanel = (SelectTypePanel)transform.parent.Find ("SelectTypePanel").GetComponent<UIPanel> ().gameObject.GetComponent(typeof(SelectTypePanel));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonClick() {
		Debug.Log ("name = " + UIButton.current.name);
		switch (UIButton.current.name) 
		{
		case "ButtonBack":
		{
			_typePanel.BackSelectTypePanel();
			transform.gameObject.SetActive(false);
			break;
		}
		case "ButtonGong":
		{
			Application.LoadLevel(1);
			break;
		}
		case "ButtonChuang":
		{
			Application.LoadLevel(1);
			break;
		}
		case "ButtonXie":
		{
			Application.LoadLevel(1);
			break;
		}
		}
	}
}
