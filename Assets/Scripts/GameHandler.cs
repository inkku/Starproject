using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

	// get input touch
	//if collides star set active
	public GameObject SelectedStar; // each star keeps track of whats within range // if moving star then fire calculate range function on selection and keep doing it while selected
	public GameObject PreviousStar; // the star I just came from
	public GameObject EarthStar;

	public int connectedStars; //if 0 earth is the SelectedStar and PreviousStar

	public GameObject selectionSprite;

	public void clickStar(GameObject starInput)
	{
		Star starInputComp = starInput.GetComponent<Star>()as Star;
		//print ( " currently selected star");
		//print (selectedStar);

		//check if star is part of your network
		if(starInputComp.connected)
		{
			//if part of network select
			setPreviousStar(SelectedStar);
			setSelectedStar(starInput);
		}

		//if not part of network see if in range
		else {			
			List<Star> inReachOfSelected = getStarsInReach(SelectedStar);
			print("in reach of selected");
			print(inReachOfSelected);
			//if in range connect
			if (inReachOfSelected.Contains(starInputComp) ){
				setSelectedStar( starInput);
				starInput.renderer.material.color = Color.red;
				starInputComp.connected = true;
			}
		}




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

	public void setSelectedStar(GameObject selStar)
	{
		SelectedStar = selStar;
		selectionSprite.transform.position = SelectedStar.transform.position;
	}

	public void setPreviousStar(GameObject curStar)
	{
		PreviousStar = curStar;
	}

	public List<Star> getStarsInReach (GameObject centerStar)
	{
		List<Star> StarInReach = new List<Star>();

		Vector3 center = centerStar.transform.position;
		float radius = centerStar.GetComponent<Star>().range;		
		var hitColliders = Physics.OverlapSphere(center, radius);		
		for (var i = 0; i < hitColliders.Length; i++) {
			//hitColliders[i].SendMessage("AddDamage");

			//print (hitColliders[i]);
			if (hitColliders[i].gameObject != SelectedStar)
			hitColliders[i].gameObject.renderer.material.color = Color.blue; //shows who is in range


			GameObject c = hitColliders[i].gameObject;
			if((c.GetComponent<Star>() as Star) != null)//if object is a star 
			{			
				Star _star =c.GetComponent<Star>() as Star;
				StarInReach.Add(_star);			
			}	
		}
		return StarInReach;
	}

	void Update () {
		
	}
}
