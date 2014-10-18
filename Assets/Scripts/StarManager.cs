using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarManager : MonoBehaviour {

    public static StarManager Instance;

    [HideInInspector] public List<Star> allStars = new List<Star>(); //ALL ALL STARS
    [HideInInspector] public List<List<Star>> starClusters = new List<List<Star>>(); //Not used right now

	public static float Score;
	public static float ScoreMax;
	public static float clusterBoost;

    public GameObject lineDrawer;

    public Texture2D[] starClasstextures = new Texture2D[7];
    public float[] starClassSizes = new float[7];
    public float[] starClassEnergies = new float[7];
    public float[] starClassGravities = new float[7];
    public float[] starClassProbabilities = new float[7];

	public bool clusterDivided;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

	void Start () 
    {
		Score = ScoreMax = (float) 0;
		clusterBoost = (float)1 ;
        //StartCoroutine(UpdateStarAges(1f));
		clusterDivided = false;
	}

	void Update()
	{
//		Score = calculateScore(GameHandler.Instance.currentStar);
	}


	public float calculateScore(Star startNode)
	{
		float tempScore = sumEnergy(startNode);

		if (!clusterDivided)
		{
			tempScore *= clusterBoost;
		}

		Debug.Log ("SCORE:" + tempScore);

		return (float) tempScore;
	}

	public float sumEnergy(Star startNode)

	{
		List<Star> MyStarCluster = GetStarCluster (startNode);

		float TotalEnergy = (float) 0;

		foreach (Star _star in MyStarCluster)
		{
			TotalEnergy += _star.energy;
		}
		return TotalEnergy;
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
