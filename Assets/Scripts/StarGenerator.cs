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
        if (Input.GetKeyDown(KeyCode.Space)) AddNewAreas(worldRadius + 20, 20);
	}


    void GenerateNewWorld()
    {
        List<Vector2> _previousSpawns = new List<Vector2>();

        for (int i = 0; i < starAmount; i++)
        {
            Vector2 _pos = Random.insideUnitCircle * worldRadius;


            bool _acceptedSpawn = true;

            if(_previousSpawns.Count > 0)
            {
                int _loops = 0;
                _acceptedSpawn = false;
                while (_acceptedSpawn == false)
                {
                    if (_loops > 40) break;

                    foreach (Vector2 _oldPos in _previousSpawns)
                    {
                        if (Vector2.Distance(_pos, _oldPos) <= minDistance)
                        {
                            _pos = Random.insideUnitCircle * worldRadius;
                            break;
                        }

                        _acceptedSpawn = true;
                    }

                    _loops++;
                }
            }

            if(_acceptedSpawn)
            {
                Instantiate(star, new Vector3(_pos.x, 0, _pos.y), Quaternion.identity);
                _previousSpawns.Add(_pos);
            }
        }
    }

    void AddNewAreas(float _minRadius, float _starAmount)
    {
        List<Vector2> _previousSpawns = new List<Vector2>();

        for (int i = 0; i < _starAmount; i++)
        {
            Vector2 _pos = Random.insideUnitCircle * (worldRadius + _minRadius);


            bool _acceptedSpawn = true;

            if(_previousSpawns.Count > 0)
            {
                int _loops = 0;
                _acceptedSpawn = false;
                while (_acceptedSpawn == false)
                {
                    if (_loops > 40) break;

                    foreach (Vector2 _oldPos in _previousSpawns)
                    {
                        if (Vector2.Distance(_pos, _oldPos) <= minDistance || Vector2.Distance(_pos, Vector2.zero) <= _minRadius)
                        {
                            Debug.Log("Distance: " + Vector2.Distance(_pos, _oldPos));
                            Debug.Log("minDistance: " + minDistance);
                            _pos = Random.insideUnitCircle * (worldRadius + _minRadius);
                            break;
                        }

                        _acceptedSpawn = true;
                    }

                    _loops++;
                }
            }

            

            if(_acceptedSpawn)
            {
                Instantiate(star, new Vector3(_pos.x, 0, _pos.y), Quaternion.identity);
                _previousSpawns.Add(_pos);
            }
        }
    }
}
