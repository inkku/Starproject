using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StarManager : MonoBehaviour {

    public static StarManager Instance;

    [HideInInspector] public List<Star> allStars;

    public List<float> starClassEnergies;
    public List<float> starClassGravities;
    public List<float> starClassProbabilities;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

	void Start () 
    {
        StartCoroutine(UpdateStarAges(1f));
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
}
