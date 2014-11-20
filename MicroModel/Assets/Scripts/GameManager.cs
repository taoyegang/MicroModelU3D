using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	public GameObject target;
	
	void Start () {
		Screen.orientation = ScreenOrientation.Landscape;
		Component[] renderers = target.GetComponentsInChildren (typeof(Renderer));
		foreach (Renderer renderer in renderers) {
			//Debug.Log("r = " + renderer.material.color.r);
			//renderer.material.shader = Shader.Find("Transparent/Diffuse");
			renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);		

		}

	}



	// Update is called once per frame
	void Update () {
	
	}
}
