using UnityEngine;
using System.Collections;

public class PopupScrollSizePanel : MonoBehaviour {

	// UI variables
	private UISlider _sliderX;
	private UISlider _sliderY;
	private UILabel _labelX;
	private UILabel _labelY;

	// Working variables
	public int minSizeX = 10, maxSizeX = 200;
	public int minSizeY = 10, maxSizeY = 200;
	
	// Methods
	void Awake () {
		// Set variables
		_sliderX = transform.Find ("SliderX").GetComponent<UISlider> ();
		_sliderY = transform.Find ("SliderY").GetComponent<UISlider> ();
		_labelX = transform.Find ("LabelX").GetComponent<UILabel> ();
		_labelY = transform.Find ("LabelY").GetComponent<UILabel> ();
//		detailLabel = transform.Find ("Detail").GetComponent<UILabel> ();
//		
//		// Update UI
//		detailLabel.SetLineSpacing (15);
	}
	
	// UI Event Callback
	public void OnButtonClicked (string name) {
		if (name.Equals ("Button Close", System.StringComparison.OrdinalIgnoreCase)) {
			GameObject.Destroy (this.gameObject);
		}
	}
	
	public void OnSlider_X_Changed () {
		float factor = _sliderX.sliderValue;
		int sizeX = Mathf.RoundToInt (Mathf.Lerp (minSizeX, maxSizeX, factor));
		_labelX.text = "x = " + sizeX.ToString();
	}

	public void OnSlider_Y_Changed () {
		float factor = _sliderY.sliderValue;
		int sizeY = Mathf.RoundToInt (Mathf.Lerp (minSizeY, maxSizeY, factor));
		_labelY.text = "y = " + sizeY.ToString();
	}
}
