using UnityEngine;
using System.Collections;

public class ButtonPillar : MonoBehaviour {

	public GameObject spawnObject;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnClick() {
		GameObject pillar = Object.Instantiate(spawnObject) as GameObject;
		pillar.layer = 8;
		pillar.transform.position = GameObject.Find ("Base").transform.position;
		pillar.transform.localScale = new Vector3 (1.5f, 1.5f, 1.49f);
		
	}
}
