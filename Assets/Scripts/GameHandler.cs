using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

    public static GameHandler Instance;

	// get input touch
	//if collides star set active
	public Star currentStar; // each star keeps track of whats within range // if moving star then fire calculate range function on selection and keep doing it while selected
    public Star previousStar; // the star I just came from

	public int connectedStars; //if 0 earth is the SelectedStar and PreviousStar

	//public GameObject selectionSprite;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        currentStar = StarManager.Instance.allStars[0];
    }

    void Update()
    {
        Star _star;
        if (Input.GetKeyDown(KeyCode.Mouse0) && MouseIsHoveringOver<Star>(out _star))
        {
            ClickStar(_star.gameObject);
        }
    }

	public void ClickStar(GameObject clickedObject)
	{
        Star clickedStar = clickedObject.GetComponent<Star>() as Star;


		//check if star is part of your network
        if (clickedStar.connected)
		{
			//if part of network, select
            SetPreviousStar(currentStar);
            SetSelectedStar(clickedStar);
		}

		//if not part of network see if in range
		else 
        {
            List<Star> inReach = GetStarsInReach(currentStar);

			//if in range connect
            if (inReach.Contains(clickedStar))
            {
                SetSelectedStar(clickedStar);
                clickedObject.renderer.material.color = Color.red;
                clickedStar.connected = true;
			}
		}

        if (previousStar != null)
        {
            if (previousStar.inMyConnections(currentStar) == false)
            {
                previousStar.connections.Add(currentStar);
                currentStar.connections.Add(previousStar);
                Debug.Log("New connection made.");
            }
        }
	}

	public void SetSelectedStar(Star _star)
	{
        currentStar = _star;
        _star.TurnOnRing(2);
    }

	public void SetPreviousStar(Star _star)
	{
		previousStar = _star;
        _star.TurnOffRing();
	}

	public List<Star> GetStarsInReach (Star _selectedStar)
	{
		List<Star> _starsInReach = new List<Star>();

        Vector3 _center = _selectedStar.transform.position;
        float _radius = _selectedStar.GetComponent<Star>().range;
        var _hitColliders = Physics.OverlapSphere(_center, _radius);

        for (var i = 0; i < _hitColliders.Length; i++) 
        {
            if (_hitColliders[i].gameObject != currentStar.gameObject)
            {
                if (_hitColliders[i].GetComponent<Star>())
                    _hitColliders[i].GetComponent<Star>().TurnOnRing(1);
            }

            GameObject _hit = _hitColliders[i].gameObject;
            if ((_hit.GetComponent<Star>())) //if object is a star 
			{
                Star _star = _hit.GetComponent<Star>() as Star;
                _starsInReach.Add(_star);			
			}	
		}
        return _starsInReach;
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
}
