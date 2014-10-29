using UnityEngine;
using System.Collections;

public class Primitive : MonoBehaviour {

	public enum PrimitiveType
	{
		UNKNOWN 	= -1,
		BOARD 		= 0,
		PILLAR 		= 1,
		WEDGE 		= 2,
		BLOCK		= 3,
	}; 

	public enum PrimitiveState
	{
		STATIC = 0,
		DYNAMIC = 1,
		MATCH = 2,
	};

	public PrimitiveState _state = PrimitiveState.STATIC;
	public PrimitiveType _type = PrimitiveType.UNKNOWN;
	public float _width = 0.0f;
	public float _height = 0.0f;
	public float _length = 0.0f;
	public bool _isActive = true;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		GameObject target = other.gameObject;
		Primitive primitive = (Primitive )target.GetComponent(typeof(Primitive));
		if (primitive.getState () == PrimitiveState.STATIC && _state == PrimitiveState.DYNAMIC) {
			if (primitive.getType () == _type && primitive.getLength () == _length && primitive.getWidth () == _width && primitive.getHeight () == _height) {
				Debug.Log ("match");
				_isActive = false;
				Color color = new Color(target.renderer.material.color.r, target.renderer.material.color.g, target.renderer.material.color.b, 1.0f);
				target.renderer.material.color = Color.green;
				primitive.setState(PrimitiveState.MATCH);
				gameObject.renderer.enabled = false;
				//target.SendMessage("RevertToNormal", _type);
			}
		}
	}

	void RevertToNormal(PrimitiveType type)
	{
		Primitive primitive = (Primitive )gameObject.GetComponent(typeof(Primitive));
		Debug.Log ("RevertToNormal");
		if (primitive.getType () == type && primitive.getState() == PrimitiveState.MATCH) {
			gameObject.renderer.material.color = Color.green;
		}
	}

	void onTriggerStay(Collider other)
	{
		
	}
	
	
	void OnTriggerExit(Collider other)
	{

	}
	
	public bool getIsActive()
	{
		return _isActive;
	}

	public void setIsActive(bool status)
	{
		_isActive = status;
	}

	public float getWidth()
	{
		return _width;
	}

	public void setWidth(float width)
	{
		_width = width;
	}

	public float getHeight()
	{
		return _height;
	}

	public void setHeight(float height)
	{
		_height = height;
	}

	public float getLength()
	{
		return _length;
	}

	public void setLength(float length)
	{
		_length = length;
	}

	public void setType(PrimitiveType type)
	{
		_type = type;
	}

	public PrimitiveType getType()
	{
		return _type;
	}

	public void setState(PrimitiveState state)
	{
		_state = state;
	}
	
	public PrimitiveState getState()
	{
		return _state;
	}

}
