using UnityEngine;
using System.Collections;

public class MouseInput : MonoBehaviour {

	//Star _current;
	//Star _previous;
	public GameHandler InputManaging;


	void Start () {
		InputManaging = GameObject.Find("GameHandlers").GetComponent<GameHandler>();
	}

	/*
	void Update () {
	
	}*/

	void OnMouseDown() {

		InputManaging.clickStar(gameObject);
		/*
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
*/
	}
}
