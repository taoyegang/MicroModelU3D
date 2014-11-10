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
				int height = panel.getHeight();
				int length = panel.getLength();
				Debug.Log("height =" + height);
				Debug.Log("length = " + length);
				
				createObject(_selectedType, length, height);
				_popupScroll.transform.gameObject.SetActive(false);
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
				createObject(_selectedType, 100, 0);
				break;
			}
			case "Label_80":{
				createObject(_selectedType, 80, 0);
				break;
			}
			case "Label_50":{
				createObject(_selectedType, 50, 0);
				break;
			}
			case "Label_30":{
				createObject(_selectedType, 30, 0);
				break;
			}
			case "ButtonRotateLeft": {
				break;
			}
			case "ButtonRotateRight": {
				break;
			}
			case "ButtonRotateUp": {
				break;
			}
			case "ButtonRotateDown": {
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

	void createObject(selectedType type, float length, float height) {
		GameObject cube = null;
		Primitive primitive = null;
		Vector3 scale = new Vector3(1,1,1);
		float factor = 0.0f;
		if(type == selectedType.wedge) {
			cube = Instantiate(Resources.Load("Wedge")) as GameObject;
			primitive = (Primitive )cube.GetComponent(typeof(Primitive));
			factor = length / primitive.getLength();
			scale = new Vector3 (3.0f, 3.0f * factor, 3.0f);
			primitive.setWidth(primitive.getWidth());
			primitive.setHeight(primitive.getHeight());
			primitive.setLength(length);
		}else if(type == selectedType.block) {
			cube = Instantiate(Resources.Load("Block")) as GameObject;
			primitive = (Primitive )cube.GetComponent(typeof(Primitive));
			factor = length / primitive.getLength();
			scale = new Vector3 (3.0f, 3.0f, 0.24f) * factor;
			primitive.setWidth(length);
			primitive.setHeight(length);
			primitive.setLength(length);
		}else if(type == selectedType.pillar) {
			cube = Instantiate(Resources.Load("Pillar")) as GameObject;
			primitive = (Primitive )cube.GetComponent(typeof(Primitive));
			factor = length / primitive.getHeight();
			Debug.Log("factor = " + factor);
			scale = new Vector3 (3.0f, 3.0f, 3.0f * factor);
			primitive.setHeight(length);
			primitive.setWidth(primitive.getWidth());
			primitive.setLength(primitive.getLength());
		}else if(type == selectedType.board) {
			cube = Instantiate(Resources.Load("Board")) as GameObject;
			primitive = (Primitive )cube.GetComponent(typeof(Primitive));
			factor = length / primitive.getLength();
			scale = new Vector3 (3.0f * factor, 6.0f * factor, 3.0f);
			primitive.setHeight(length);
			primitive.setWidth(primitive.getWidth());
			primitive.setLength(length);
			cube.transform.Rotate (Vector3.right * 90);
			
			
		}

		if(primitive != null)
		{
			CameraController controller = (CameraController)Camera.main.GetComponent(typeof(CameraController));
			primitive._cameraController = controller;
		}
		
		if(cube != null) {
			cube.layer = 8;
			cube.transform.position = GameObject.Find ("Base").transform.position;
			cube.transform.localScale = scale;
		}

		_popupSize.transform.gameObject.SetActive(false);

	}
}
