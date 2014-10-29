using UnityEngine;
using System.Collections;

public class ButtonBlock : MonoBehaviour {

	public GameObject spawnObject;
	
	void OnClick() {
		GameObject block = Object.Instantiate(spawnObject) as GameObject;
		block.layer = 8;
		block.transform.localScale = new Vector3 (1.5f, 1.5f, 0.12f);
		block.transform.position = GameObject.Find ("Base").transform.position;

	}
}
