using UnityEngine;
using System.Collections;

public class ButtonPillar : MonoBehaviour {

	public GameObject spawnObject;
	

	void OnClick() {
		GameObject pillar = Object.Instantiate(spawnObject) as GameObject;
		pillar.name = "Pillar";
		pillar.layer = 8;
		pillar.transform.position = GameObject.Find ("Base").transform.position;
		pillar.transform.localScale = new Vector3 (3.0f, 3.0f, 2.0f);
		
	}
}
