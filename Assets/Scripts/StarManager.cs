using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarManager : MonoBehaviour {

    public static StarManager Instance;

    [HideInInspector] public List<Star> allStars;

    public List<float> starClassEnergies;
    public List<float> starClassGravities;
    public List<float> starClassProbabilities;

	public bool clusterDivided;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

	void Start () 
    {
        StartCoroutine(UpdateStarAges(1f));
		clusterDivided = false;
	}


    IEnumerator UpdateStarAges(float _updateFreq)
    {
        for (; ;)
        {
            foreach (Star _star in allStars)
            {
                //Update age for each star
            }

            yield return new WaitForSeconds(_updateFreq);
        }
    }

	public List<Star> connectedStars (Star startNode) 
	{

		List<Star> connectedTemp = new List<Star>();

		foreach (Star _star in allStars)
		{
			_star.connected = false;
		}

		this.FloodConnected(startNode);

		foreach (Star _star in startNode.connections) 
		{
			if(_star.connected)
			{
				connectedTemp.Add(_star);
			}
			else
			{
				clusterDivided = true;
		
			}
		}
		return connectedTemp;
	}






	//************* private utility functions ************

	private void FloodConnected(Star startNode)
 	{
		startNode.connected = true;

		foreach (Star _star in startNode.connections) 
		{
			if(!_star.connected)
			{
				FloodConnected(_star);
			}
		}
	}
}
