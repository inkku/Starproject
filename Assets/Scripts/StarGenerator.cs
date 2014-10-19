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

    void Start()
    {
        GenerateNewWorld();
    }

    void GenerateNewWorld()
    {
        List<Vector3> _previousSpawns = new List<Vector3>();

        for (int i = 0; i < starAmount; i++)
        {
            Vector3 _pos = Random.insideUnitSphere * worldRadius;


            bool _acceptedSpawn = true;

            if(_previousSpawns.Count > 0)
            {
                int _loops = 0;
                _acceptedSpawn = false;
                while (_acceptedSpawn == false)
                {
                    if (_loops > 40) break;

                    foreach (Vector3 _oldPos in _previousSpawns)
                    {
                        if (Vector3.Distance(_pos, _oldPos) <= minDistance)
                        {
                            _pos = Random.insideUnitSphere * worldRadius;
                            break;
                        }

                        _acceptedSpawn = true;
                    }

                    _loops++;
                }
            }

            if(_acceptedSpawn)
            {
                object _tmp = Instantiate(star, new Vector3(_pos.x, _pos.y, _pos.z), Quaternion.identity);
                StarManager.Instance.allStars.Add((_tmp as GameObject).GetComponent<Star>());

                _previousSpawns.Add(_pos);
            }
        }

        StarManager.Instance.allStars[0].transform.position = Vector3.zero;
    }
    void AddNewAreas(float _minRadius, float _starAmount)
    {
        List<Vector3> _previousSpawns = new List<Vector3>();

        for (int i = 0; i < _starAmount; i++)
        {
            Vector3 _pos = Random.insideUnitSphere * (worldRadius + _minRadius);


            bool _acceptedSpawn = true;

            if(_previousSpawns.Count > 0)
            {
                int _loops = 0;
                _acceptedSpawn = false;
                while (_acceptedSpawn == false)
                {
                    if (_loops > 40) break;

                    foreach (Vector3 _oldPos in _previousSpawns)
                    {
                        if (Vector3.Distance(_pos, _oldPos) <= minDistance || Vector3.Distance(_pos, Vector3.zero) <= _minRadius)
                        {
                            _pos = Random.insideUnitSphere * (worldRadius + _minRadius);
                            break;
                        }

                        _acceptedSpawn = true;
                    }

                    _loops++;
                }
            }

            if(_acceptedSpawn)
            {
                object _tmp = Instantiate(star, new Vector3(_pos.x, _pos.y, _pos.z), Quaternion.identity);
                StarManager.Instance.allStars.Add((_tmp as GameObject).GetComponent<Star>());

                _previousSpawns.Add(_pos);
            }
        }

        StarManager.Instance.allStars[0].transform.position = Vector3.zero;
    }
}
