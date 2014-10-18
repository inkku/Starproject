using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

    public static GameHandler Instance;


//	public int connectedStars;
	public Star currentStar;
    public Star previousStar; 


    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        SetCurrentStar(StarManager.Instance.allStars[0]);
		Debug.Log ("AGE: " + currentStar.age);
        StartCoroutine(FindStarsInReach(0.5f));
    }

    void Update()
    {
        Star _star;
        if (Input.GetKeyDown(KeyCode.Mouse0) && MouseIsHoveringOver<Star>(out _star))
        {
            LeftClickStar(_star.gameObject);
        }

		else if (Input.GetKeyDown(KeyCode.Mouse1) && MouseIsHoveringOver<Star>(out _star))
		{
			RightClickStar(_star.gameObject);

		}

    }

	public void RightClickStar(GameObject clickedObject)
	{

		// Om i systemet: välj och gå till
		// Om inte connected till previous: connect
		// Om inte i systemet: nada

	    Star clickedStar = clickedObject.GetComponent<Star>();

		// within reach
		if (currentStar.starsInReach.Contains(clickedStar))
		{
			SetPreviousStar(currentStar);
			SetCurrentStar(clickedStar);

			if (previousStar.connections.Contains(currentStar) == false)
			{
				previousStar.connections.Add(currentStar);
				currentStar.connections.Add(previousStar);
			}
		}
	}

	public void LeftClickStar(GameObject clickedObject)
	{
		// If within reach && within system -> show stat window
		//clickedObject.renderer.material.color = Color.red;

	}


	//************ UTILITY ************


	IEnumerator FindStarsInReach(float _updateFreq)
	{
		for (; ; )
		{
			currentStar.GetReach();
			yield return new WaitForSeconds(_updateFreq);
		}
	}

	public bool MouseIsHoveringOver<T>() where T : Component
	{
		Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit _rayHit;
		if (Physics.Raycast(_ray, out _rayHit, 1500f))
		{
			if (_rayHit.collider.GetComponent<T>())
				return true;
		}
		
		return false;
	}

	public bool MouseIsHoveringOver<T>(out T _type) where T : Component
	{
		Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit _rayHit;
		if (Physics.Raycast(_ray, out _rayHit, 1500f))
		{
			if (_rayHit.collider.GetComponent<T>())
			{
				_type = _rayHit.collider.GetComponent<T>();
				return true;
			}
		}
		
		_type = null;
		return false;
	}





	//************ HOUSEKEEPING ************
	
    public void SetCurrentStar(Star _star)
	{
        currentStar = _star;
        _star.state = Star.State.Connected;
        currentStar.current = true;
    }

	public void SetPreviousStar(Star _star)
	{
		previousStar = _star;
        previousStar.current = false;
	}
	 
}
