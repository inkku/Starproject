using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

	Star _current;
	Star _previous;

	// Use this for initialization
	void Start () {
		_current = null;
		_previous = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	void OnMouseDown() {

		_previous = _current;
		_current = this.gameObject.GetComponent<Star>();
		
		Debug.Log("Pressed left click.");
		this.renderer.material.color = Color.red; 

		if (_previous != null) 
		{
			if(!_previous.inMyConnections(_current))
			{
				_previous.connections.Add (_current);
				_current.connections.Add (_previous);
				Debug.Log ("New connection made.");
			}
		}

	}
}
