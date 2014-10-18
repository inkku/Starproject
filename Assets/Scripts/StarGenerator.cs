using UnityEngine;
using System.Collections.Generic;

public class StarGenerator : MonoBehaviour {

    public static StarGenerator Instance;


    public GameObject star;

    public int worldRadius;
    public int starAmount;
    public int minDistance;

    void Awake()
    {
        if (Instance) Destroy(this);
        else Instance = this;
    }

	// Use this for initialization
	void Start () 
    {
        GenerateNewWorld();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void GenerateNewWorld()
    {
        List<Vector2> _previousSpawns = new List<Vector2>();

        for (int i = 0; i < starAmount; i++)
        {
            bool _acceptedSpawn = false;

            Vector2 _pos = Random.insideUnitCircle * worldRadius;


            int _loops = 0;
            while(_acceptedSpawn == false)
            {
                if (_loops > 10) break;

                foreach (Vector2 _oldPos in _previousSpawns)
                {

                    if (Vector2.Distance(_pos, _oldPos) <= minDistance)
                    {
                        _pos = Random.insideUnitCircle * worldRadius;
                    }

                    else _acceptedSpawn = true;
                }

                _loops++;
            }

            Instantiate(star, new Vector3(_pos.x, 0, _pos.y), Quaternion.identity);
            _previousSpawns.Add(_pos);

        }

    }
}
