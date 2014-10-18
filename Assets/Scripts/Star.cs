using UnityEngine;
using System.Collections.Generic;

public class Star : MonoBehaviour {

    public enum Type { cl0, cl1, cl2, cl3, cl4, cl5, cl6, cl7, cl8, cl9 }
    public Type type;
	
    public List<Star> connections;
	public float age;
	public float energy
	{	  
        get
        {
            switch (type)
            {
                case Type.cl0:
                    return StarManager.Instance.starClassEnergies[0];

                case Type.cl1:
                    return StarManager.Instance.starClassEnergies[1];

                case Type.cl2:
                    return StarManager.Instance.starClassEnergies[2];

                case Type.cl3:
                    return StarManager.Instance.starClassEnergies[3];

                case Type.cl4:
                    return StarManager.Instance.starClassEnergies[4];

                case Type.cl5:
                    return StarManager.Instance.starClassEnergies[5];

                case Type.cl6:
                    return StarManager.Instance.starClassEnergies[6];

                case Type.cl7:
                    return StarManager.Instance.starClassEnergies[7];

                case Type.cl8:
                    return StarManager.Instance.starClassEnergies[8];

                case Type.cl9:
                    return StarManager.Instance.starClassEnergies[9];

                default:
                    return 0;
            }
        }
    }
    public float range
    {
        get
        {
            switch (type)
            {
                case Type.cl0:
                    return StarManager.Instance.starClassGravities[0];

                case Type.cl1:
                    return StarManager.Instance.starClassGravities[1];

                case Type.cl2:
                    return StarManager.Instance.starClassGravities[2];

                case Type.cl3:
                    return StarManager.Instance.starClassGravities[3];

                case Type.cl4:
                    return StarManager.Instance.starClassGravities[4];

                case Type.cl5:
                    return StarManager.Instance.starClassGravities[5];

                case Type.cl6:
                    return StarManager.Instance.starClassGravities[6];

                case Type.cl7:
                    return StarManager.Instance.starClassGravities[7];

                case Type.cl8:
                    return StarManager.Instance.starClassGravities[8];

                case Type.cl9:
                    return StarManager.Instance.starClassGravities[9];

                default:
                    return 0;
            }
        }
    }

    public GameObject dysonSphere;
    public GameObject selectRing;
    public float selectRingScale;

    //public LineRenderer line;

    public enum State
    { 
        Connected,
        LocallyConnected,
        NotConnected
    }

    public State state = State.NotConnected;

    public List<Star> starsInReach;

    public bool current;


    void Awake()
    {
        //line = GetComponent<LineRenderer>();
    }

    void Start()
    {
        selectRingScale = selectRing.transform.localScale.x;

        Setup();
    }

    void Update()
    {
        foreach (Star _star in connections)
        {
			Debug.DrawRay(_star.transform.position, this.transform.position);
            //LineRenderer line = gameObject.AddComponent<LineRenderer>();
            //line.SetPosition(1, _star.transform.position);
        }
    }

    void HandleStates()
    { 
        switch(state)
        {
            case State.NotConnected:
                dysonSphere.SetActive(false);
                TurnOffRing();
                break;

            case State.Connected:
                dysonSphere.SetActive(true);

                if (GameHandler.Instance.currentStar == this)
                    TurnOnRing(2);
                else if (GameHandler.Instance.currentStar.starsInReach.Contains(this))
                    TurnOnRing(1);
                else
                    TurnOffRing();
                break;

            case State.LocallyConnected:
                dysonSphere.SetActive(true);

                if (GameHandler.Instance.currentStar.starsInReach.Contains(this))
                    TurnOnRing(1);
                else
                    TurnOffRing();
                break;
        }
    }

    void Setup()
    {
        if (StarManager.Instance.allStars.Contains(this) == false)
            StarManager.Instance.allStars.Add(this);

        int _rand = Random.Range(0, 101);

        if (_rand <= StarManager.Instance.starClassProbabilities[0]) type = Type.cl0;
        else if (_rand <= StarManager.Instance.starClassProbabilities[1]) type = Type.cl1;
        else if (_rand <= StarManager.Instance.starClassProbabilities[2]) type = Type.cl2;
        else if (_rand <= StarManager.Instance.starClassProbabilities[3]) type = Type.cl3;
        else if (_rand <= StarManager.Instance.starClassProbabilities[4]) type = Type.cl4;
        else if (_rand <= StarManager.Instance.starClassProbabilities[5]) type = Type.cl5;
        else if (_rand <= StarManager.Instance.starClassProbabilities[6]) type = Type.cl6;
        else if (_rand <= StarManager.Instance.starClassProbabilities[7]) type = Type.cl7;
        else if (_rand <= StarManager.Instance.starClassProbabilities[8]) type = Type.cl8;
        else if (_rand <= StarManager.Instance.starClassProbabilities[9]) type = Type.cl9;


        age = Random.Range(1, 13); //Age depends on startype? It should >.>

        switch (type)
        {
            case Type.cl0:
                name = "Class O Star";
                break;

            case Type.cl1:
                name = "Class B Star";
                break;

            case Type.cl2:
                name = "Class A Star";
                break;

            case Type.cl3:
                name = "Class F Star";
                break;

            case Type.cl4:
                name = "Class G Star";
                break;

            case Type.cl5:
                name = "Class K Star";
                break;

            case Type.cl6:
                name = "Class M Star";
                break;

            case Type.cl7:
                name = "Class L Star";
                break;

            case Type.cl8:
                name = "Class T Star";
                break;

            case Type.cl9:
                name = "Class Y Star";
                break;
        }

        //Setup graphics?
    }

    public void TurnOnRing(float _size)
    {
        selectRing.SetActive(true);
        selectRing.transform.localScale = new Vector3(_size, _size, _size);
    }

    public void TurnOffRing()
    {
        selectRing.SetActive(false);
        selectRing.transform.localScale = new Vector3(selectRingScale, selectRingScale, selectRingScale);
    }

    public List<Star> GetReach()
    {
        starsInReach = new List<Star>();

        var _hitColliders = Physics.OverlapSphere(transform.position, range);
        for (var i = 0; i < _hitColliders.Length; i++)
        {
            GameObject _hit = _hitColliders[i].gameObject;

            if ((_hit != gameObject && _hit.GetComponent<Star>())) //if object is a star 
            {
                Star _star = _hit.GetComponent<Star>();
                starsInReach.Add(_star);
            }
        }

        return starsInReach;
    }

    public bool IsInReach(Star _star)
    {
        GetReach();
        return starsInReach.Contains(_star);
    }
}
