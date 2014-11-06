using UnityEngine;
using System.Collections;

public class PopupScrollSizePanel : MonoBehaviour {

	// UI variables
	private UISlider _sliderX;
	private UISlider _sliderY;
	private UILabel _labelX;
	private UILabel _labelY;
	public int height = 0;
	public int length = 0;

	// Working variables
	public int minSizeX = 30, maxSizeX = 100;
	public int minSizeY = 30, maxSizeY = 100;

	public int getHeight()
	{
		return height;
	}

	public int getLength()
	{
		return length;
	}

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
		length = Mathf.RoundToInt (Mathf.Lerp (minSizeX, maxSizeX, factor));
		_labelX.text = "长度 = " + length.ToString();
	}

	public void OnSlider_Y_Changed () {
		float factor = _sliderY.sliderValue;
		height = Mathf.RoundToInt (Mathf.Lerp (minSizeY, maxSizeY, factor));
		_labelY.text = "高度 = " + height.ToString();
	}
}
