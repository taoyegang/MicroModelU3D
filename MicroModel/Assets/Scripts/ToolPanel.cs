using UnityEngine;
using System.Collections;

public class ToolPanel : MonoBehaviour {

	enum selectedType{
		wedge, 
		block,
		board, 
		pillar
	}

	private UIPanel _popupScroll;
	private UIPanel _popupSize;
	private selectedType _selectedType = selectedType.wedge;

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
				PopupScrollSizePanel panel = (PopupScrollSizePanel )_popupScroll.GetComponent(typeof(PopupScrollSizePanel));
				int width = panel.getWidth();
				int length = panel.getLength();
				int height = 50;
				Debug.Log("width = " + width);
				Debug.Log("height = " + length);
				GameObject cube = null;
				Primitive primitive = null;
				if(_selectedType == selectedType.wedge) {
					
					cube = Instantiate(Resources.Load("Wedge")) as GameObject;
				}else if(_selectedType == selectedType.block) {
					
					cube = Instantiate(Resources.Load("Block")) as GameObject;

					
				}else if(_selectedType == selectedType.pillar) {
					
					cube = Instantiate(Resources.Load("Pillar")) as GameObject;
					
				}else if(_selectedType == selectedType.board) {
					cube = Instantiate(Resources.Load("Board")) as GameObject;
				}
				
				if(cube != null) {
					cube.layer = 8;
					cube.transform.position = GameObject.Find ("Base").transform.position;
					primitive = (Primitive )cube.GetComponent(typeof(Primitive));
					float scaleWidth = width / primitive.getWidth();
					float scaleHeight = width / primitive.getHeight();
					cube.transform.localScale = new Vector3 (scaleWidth, scaleHeight, 1.0f);
					primitive.setWidth(width);
					primitive.setHeight(length);
					primitive.setHeight(height);
					_popupScroll.transform.gameObject.SetActive(false);
				}
				
				break;
			}
			case "RedButtonPillar":{
				//mutiao
				Debug.Log("木条");
				_selectedType = selectedType.pillar;
				break;
			}
			case "RedButtonWedge":{
				//xiezi
				Debug.Log("楔子");
				_selectedType = selectedType.wedge;
				break;
			}
			case "RedButtonBoard":{
				//muban
				Debug.Log("木板");
				_selectedType = selectedType.board;
				break;
			}
			case "ButtonWedge":{
				//create xiezi
				refreshPopupSize(selectedType.wedge);
				break;
			}
			case "ButtonBlock":{
				//create mukuai
				refreshPopupSize(selectedType.block);
				break;
			}
			case "ButtonBoard":{
				//create muban
				refreshPopupSize(selectedType.board);
				break;
			}
			case "ButtonPillar":{
				//mutiao
				refreshPopupSize(selectedType.pillar);	
				break;
			}
			case "Label_100":{
				createObject(_selectedType, 100);
				break;
			}
			case "Label_80":{
				createObject(_selectedType, 80);
				break;
			}
			case "Label_50":{
				createObject(_selectedType, 50);
				break;
			}
			case "Label_30":{
				createObject(_selectedType, 30);
				break;
			}
		}
	}

	void refreshPopupSize(selectedType type) {
		if(_selectedType == type)
		{
			_popupSize.transform.gameObject.SetActive(_popupSize.transform.gameObject.activeSelf == false);
		}
		else
		{
			_popupSize.transform.gameObject.SetActive(true);
			_selectedType = type;
		}
		
		_popupSize.transform.position = UIButton.current.transform.position;
	}

	void createObject(selectedType type, float length) {
		//在这里根据type和length写创建物体相关的
	}
}
