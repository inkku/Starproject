using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

    public static GameHandler Instance;

	public Star currentStar;
    public Star previousStar; 

	public int connectedStars; 


    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        SetCurrentStar(StarManager.Instance.allStars[0]);
        StartCoroutine(FindStarsInReach(0.5f));
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
        Star clickedStar = clickedObject.GetComponent<Star>();


        if (clickedStar.state == Star.State.Connected || currentStar.starsInReach.Contains(clickedStar))
		{
            SetPreviousStar(currentStar);
            SetCurrentStar(clickedStar);
		}

        if (previousStar != null)
        {
            if (previousStar.connections.Contains(currentStar) == false)
            {
                previousStar.connections.Add(currentStar);
                currentStar.connections.Add(previousStar);
                Debug.Log("New connection made.");
            }
        }
	}

    IEnumerator FindStarsInReach(float _updateFreq)
    {
        for (; ; )
        {
            currentStar.GetReach();
            yield return new WaitForSeconds(_updateFreq);
        }
    }

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
