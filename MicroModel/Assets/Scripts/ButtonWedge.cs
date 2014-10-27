using UnityEngine;
using System.Collections;

public class ButtonWedge : MonoBehaviour {
	public GameObject spawnObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		GameObject wedge = Object.Instantiate(spawnObject) as GameObject;
		wedge.layer = 8;
		wedge.transform.position = GameObject.Find ("Base").transform.position;
		wedge.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);

		
	}
}
