using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameHandler : MonoBehaviour {

    public static GameHandler Instance;

	[HideInInspector] public Star currentStar;
    [HideInInspector] public Star previousStar;

    public static float Score;
    public static float ScoreMax;
    public static float clusterBoost;
    public static float reachForce;


    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

    void Start()
    {
        SetCurrentStar(StarManager.Instance.allStars[0]);
        StartCoroutine(FindStarsInReach(0.5f));

        Score = ScoreMax = (float)0;
        clusterBoost = (float)1;
        reachForce = 1;
    }

    void Update()
    {
        if (currentStar.state != Star.State.Dying)
        {
            Star _star;
            if (Input.GetKeyDown(KeyCode.Mouse0) && MouseIsHoveringOver<Star>(out _star))
            {
                LeftClickStar(_star.gameObject);
            }

            float tempScore = CalculateScore(GameHandler.Instance.currentStar);
//            Debug.Log("SCORE:" + tempScore);
        }
    }

	public void LeftClickStar(GameObject clickedObject)
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


	//************ UTILITY ************

    public float CalculateScore(Star startNode)
    {
        float tempScore = StarManager.Instance.SumEnergy(startNode);

        if (StarManager.Instance.clusterDivided == false)
        {
            tempScore *= clusterBoost;
        }

        Debug.Log("SCORE:" + tempScore);

        return (float)tempScore;
    }

	IEnumerator FindStarsInReach(float _updateFreq)
	{
		for (; ; )
		{
            if(currentStar.state != Star.State.Dying)
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

        if (currentStar.unDiscovered)
        {
            currentStar.Discover();
        }
    }

	public void SetPreviousStar(Star _star)
	{
		previousStar = _star;
        previousStar.current = false;
	}
	 
}
