using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarManager : MonoBehaviour {

    public static StarManager Instance;

    [HideInInspector] public List<Star> allStars = new List<Star>();
    [HideInInspector] public List<List<Star>> starClusters = new List<List<Star>>(); //Not used right now

    public Texture2D[] starClasstextures = new Texture2D[10];
    public float[] starClassEnergies = new float[10];
    public float[] starClassGravities = new float[10];
    public float[] starClassProbabilities = new float[10];

	public bool clusterDivided;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

	void Start () 
    {
        //StartCoroutine(UpdateStarAges(1f));
		clusterDivided = false;
	}

    //IEnumerator UpdateStarAges(float _updateFreq)
    //{
    //    for (; ;)
    //    {
    //        foreach (Star _star in allStars)
    //        {
    //            //Update age for each star
    //        }

    //        yield return new WaitForSeconds(_updateFreq);
    //    }
    //}

	public List<Star> GetStarCluster (Star startNode) 
	{
		List<Star> connectedTemp = new List<Star>();


		foreach (Star _star in allStars)
		{
            _star.state = Star.State.NotConnected;
		}

		this.FloodConnected(startNode);

		foreach (Star _star in startNode.connections) 
		{
			if(_star.state == Star.State.Connected)
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
		startNode.state = Star.State.Connected;

		foreach (Star _star in startNode.connections) 
		{
			if(_star.state == Star.State.Connected)
			{
				FloodConnected(_star);
			}
		}
	}
}
