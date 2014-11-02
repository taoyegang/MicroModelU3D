using UnityEngine;
using System.Collections;

public class ToolPanel : MonoBehaviour {

	enum selectedType{
		selectedType_xiezi, //xiezi
		selectedType_mukuai, //mukuai
		selectedType_muban, //muban
		selectedType_mutiao //mutiao
	}

	private UIPanel _popupScroll;
	private UIPanel _popupSize;
	private selectedType _selectedType = selectedType.selectedType_xiezi;

	// Use this for initialization
	void Start () {
		_popupScroll = transform.Find ("PopupScrollSizePanel").GetComponent<UIPanel> ();
		_popupScroll.transform.gameObject.SetActive(false);
		_popupSize = transform.Find ("PopupSizePanel").GetComponent<UIPanel> ();
		_popupSize.transform.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonClick()
	{
		Debug.Log ("name = " + UIButton.current.name);
		switch (UIButton.current.name) 
		{
			case "ButtonSaw":
			{
			if(_popupScroll.transform.gameObject.activeSelf == false)
			{
				_popupScroll.transform.gameObject.SetActive(true);
			}
			break;
			}
		case "ButtonGo":
		{
			// 根据选择的类型create object
			// 
			_popupScroll.transform.gameObject.SetActive(false);
			break;
		}
		case "RedButtonPillar":{
			//mutiao
			_selectedType = selectedType.selectedType_mutiao;
			break;
		}
		case "RedButtonWedge":{
			//xiezi
			_selectedType = selectedType.selectedType_xiezi;
			break;
		}
		case "RedButtonBoard":{
			//muban
			_selectedType = selectedType.selectedType_muban;
			break;
		}
		case "ButtonWedge":{
			//create xiezi
			_selectedType = selectedType.selectedType_xiezi;
			_popupSize.transform.gameObject.SetActive(_popupSize.transform.gameObject.activeSelf == false);
			break;
		}
		case "ButtonBlock":{
			//create mukuai
			_selectedType = selectedType.selectedType_mukuai;
			_popupSize.transform.gameObject.SetActive(_popupSize.transform.gameObject.activeSelf == false);
			break;
		}
		case "ButtonBoard":{
			//create muban
			_selectedType = selectedType.selectedType_muban;
			_popupSize.transform.gameObject.SetActive(_popupSize.transform.gameObject.activeSelf == false);
			break;
		}
		case "ButtonPillar":{
			//mutiao
			_selectedType = selectedType.selectedType_mutiao;
			_popupSize.transform.gameObject.SetActive(_popupSize.transform.gameObject.activeSelf == false);
			break;
		}
		}
	}
}
