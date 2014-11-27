using UnityEngine;
using System.Collections;

public class SelectTearPanel : MonoBehaviour {

	SelectTypePanel _typePanel;

	// Use this for initialization
	void Start () {
		_typePanel = (SelectTypePanel)transform.parent.Find ("SelectTypePanel").GetComponent<UIPanel> ().GetComponent(typeof(SelectTypePanel));
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
		case "ButtonCircle":
		{
			Application.LoadLevel(1);
			break;
		}
		case "ButtonRectangle":
		{
			Application.LoadLevel(1);
			break;
		}
		case "ButtonTriangle":
		{
			Application.LoadLevel(1);
			break;
		}
		case "ButtonSafe":
		{
			Application.LoadLevel(1);
			break;
		}
		}
	}
}
