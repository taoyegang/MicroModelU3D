using UnityEngine;
using System.Collections;

public class ButtonBoard : MonoBehaviour {

	public GameObject spawnObject;
	

	void OnClick() {
		GameObject board = Object.Instantiate(spawnObject) as GameObject;
		board.layer = 8;
		board.transform.position = GameObject.Find ("Base").transform.position;
		board.transform.localScale = new Vector3 (1.5f, 3.0f, 1.5f);
		board.transform.Rotate (Vector3.right * 90);
	}
}
