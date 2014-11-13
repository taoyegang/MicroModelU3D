using UnityEngine;
using System.Collections;

public class SelectTypePanel : MonoBehaviour {

	enum SelectTypePage
	{
		SelectTypePage_Type,
		SelectTypePage_Tear,
		SelectTypePage_Support,
	}
	
	private UIPanel _tearPanel;
	private UIPanel _supportPanel;
	SelectTypePage _curPage;

	// Use this for initialization
	void Start () {
		_tearPanel = transform.parent.Find ("SelectTearPanel").GetComponent<UIPanel> ();
		_supportPanel = transform.parent.Find ("SelectSupportPanel").GetComponent<UIPanel> ();
//		_supportPanel.transform.gameObject.SetActive(false);
		_curPage = SelectTypePage.SelectTypePage_Type;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void BackSelectTypePanel() {
		_curPage = SelectTypePage.SelectTypePage_Type;
		transform.gameObject.SetActive(true);
	}
	
	public void OnButtonClick() {
		Debug.Log ("name = " + UIButton.current.name);
		switch (UIButton.current.name) 
		{
		case "ButtonSupport":
		{
			if(_curPage == SelectTypePage.SelectTypePage_Type)
			{
				_curPage = SelectTypePage.SelectTypePage_Support;
				_supportPanel.gameObject.SetActive(true);
				transform.gameObject.SetActive(false);
			}
			break;
		}
		case "ButtonTear":
		{
			if(_curPage == SelectTypePage.SelectTypePage_Type)
			{
				_curPage = SelectTypePage.SelectTypePage_Tear;
				_tearPanel.gameObject.SetActive(true);
				transform.gameObject.SetActive(false);
			}
			break;
		}
		case "ButtonBack":
		{
			if(_curPage == SelectTypePage.SelectTypePage_Type)
			{
				Application.LoadLevel(0);
			}
			break;
		}
		}
	}
}
