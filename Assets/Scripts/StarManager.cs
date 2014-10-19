using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarManager : MonoBehaviour {

    public static StarManager Instance;

    [HideInInspector] public List<Star> allStars = new List<Star>(); //ALL ALL STARS
    [HideInInspector] public List<Star> discoveredStars = new List<Star>();

    public GameObject lineDrawer;
    public ParticleSystem fx_Death1;
    public ParticleSystem fx_Death2;

    public float[] starClassSizes = new float[7];
    public float[] starClassEnergies = new float[7];
    public Vector2[] starClassLifespans = new Vector2[7];
    public float[] starClassGravities = new float[7];
    public float[] starClassProbabilities = new float[7];

	[HideInInspector] public bool clusterDivided;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

	void Start () 
    {
        clusterDivided = false;

        StartCoroutine(StarManager.Instance.UpdateStarAges(0.3f));
	}

    IEnumerator UpdateStarAges(float _updateFreq)
    {
        for (; ; )
        {
            for (int i = 0; i < allStars.Count; i++)
            {
                if (allStars[i].unDiscovered)
                    continue;

                allStars[i].ageXten += Time.deltaTime;

                if (allStars[i].ageXten > StarManager.Instance.starClassLifespans[allStars[i].typeNum].y)
                {
                    StartCoroutine(allStars[i].Die(1f));
                }
            }

            yield return new WaitForSeconds(_updateFreq);
        }
    }


	public List<Star> GetStarCluster (Star startNode) 
	{
		List<Star> connectedTemp = new List<Star>();

		foreach (Star _star in allStars)
		{
            _star.reached = false;
		}

		this.FloodConnected(startNode);

		foreach (Star _star in allStars) 
		{
			if(_star.reached)
			{
				connectedTemp.Add(_star);
			}
		}

		Debug.Log ("GetStarCluster: number of all connecteds in current network " + connectedTemp.Count);
		return connectedTemp;
	}



	public float SumEnergy(Star startNode)
	{
		List<Star> MyStarCluster = GetStarCluster (startNode);
		
		float TotalEnergy = (float) 0;
		
		foreach (Star _star in MyStarCluster)
		{
			TotalEnergy += _star.energy;
		}
		return TotalEnergy;
	}
	
	
	public float totalNumConnections(Star startNode)		
	{
		List<Star> MyStarCluster = GetStarCluster (startNode);
		
		float numConnections = (float) 0;
		float lengthConnections = (float) 0;
		
		foreach (Star _star in MyStarCluster)
		{
			numConnections += _star.connections.Count;
			
			foreach (Star s in _star.connections)
			{
				lengthConnections += Vector3.Distance (s.transform.position, _star.transform.position);
			}
			
		}
		//return lengthConnections /2;
		Debug.Log ("totalNumConnections in whole cluster: " + numConnections/2);
		return numConnections / 2;
	}

	//************* private utility functions ************


	private void FloodConnected(Star startNode)
 	{
		startNode.reached=true;

		foreach (Star _star in startNode.connections) 
		{
			if(!_star.reached)
			{
				FloodConnected(_star);
			}
		}
	}
}
