using UnityEngine;
using System.Collections;

public class StarGenerator : MonoBehaviour {

    public static StarGenerator Instance;


    public float worldSize;
    //public float star

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
