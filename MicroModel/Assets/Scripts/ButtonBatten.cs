using UnityEngine;
using System.Collections;

public class ButtonBatten : MonoBehaviour {

	int _battenCount;
	UILabel _battenLabel;

	// Use this for initialization
	void Start () {
		_battenCount = 2;
		_battenLabel = transform.Find ("LabelBatten").GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {

		if (_battenCount > 0) {
			_battenCount --;
			_battenLabel.text = "木条:" + _battenCount.ToString();

			Object obj = Resources.Load("Pillar");
			if(obj != null) {
				GameObject newObj = Object.Instantiate(obj) as GameObject;
				newObj.layer = 8;
				newObj.transform.position = new Vector3(0.6f, 0.4f, -5.0f);
			}
		}
	}
}
