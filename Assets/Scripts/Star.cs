using UnityEngine;
using System.Collections.Generic;

public class Star : MonoBehaviour {

    public enum Type { cl0, cl1, cl2, cl3, cl4, cl5, cl6 }
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

                default:
                    return 0;
            }
        }
    }

    public GameObject dysonSphere;
    public GameObject selectRing;
    float selectRingScale;

    public enum State
    { 
        Connected,
        LocallyConnected,
        NotConnected
    }

    public State state = State.NotConnected;

    public List<Star> starsInReach;

    public bool current;


    void Start()
    {
        selectRingScale = selectRing.transform.localScale.x;

        Setup();
    }

    void Update()
    {
        foreach (Star _star in connections)
        {
			if (IsConnectionDrawn(_star))
                continue;

            GameObject _line = Instantiate(StarManager.Instance.lineDrawer, transform.position, Quaternion.identity) as GameObject;
            _line.GetComponent<LineDrawer>().Draw(this.gameObject, _star.gameObject);
        }

        HandleStates();
    }

    void HandleStates()
    {
        switch(state)
        {
            case State.NotConnected:
                dysonSphere.SetActive(false);

                if (GameHandler.Instance.currentStar.starsInReach.Contains(this))
                    TurnOnRing(1);
                else
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

        switch (type)
        {
            case Type.cl0:
                name = "Type-M Star";
                transform.localScale *= StarManager.Instance.starClassSizes[0];
                age = Random.Range(0, 13);
                renderer.material.color = new Color(1, 0.160f, 0.1f); //red
                break;

            case Type.cl1:
                name = "Type-K Star";
                transform.localScale *= StarManager.Instance.starClassSizes[1];
                age = Random.Range(0, 13);
                renderer.material.color = new Color(1, 0.5f, 0.1f); //orange
                break;
                
            case Type.cl2:
                name = "Type-G Star";
                transform.localScale *= StarManager.Instance.starClassSizes[2];
                age = Random.Range(0, 9);
                renderer.material.color = new Color(1, 0.8f, 0.1f); //yellow
                break;

            case Type.cl3:
                name = "Type-F Star";
                transform.localScale *= StarManager.Instance.starClassSizes[3];
                age = Random.Range(0, 5);
                renderer.material.color = Color.white;
                break;

            case Type.cl4:
                name = "Type-A Star";
                transform.localScale *= StarManager.Instance.starClassSizes[4];
                age = Random.Range(0, 4);
                renderer.material.color = new Color(0.94f, 0.97f, 0.97f); //white-ish
                break;

            case Type.cl5:
                name = "Type-B Star";
                transform.localScale *= StarManager.Instance.starClassSizes[5];
                age = Random.Range(0, 3);
                renderer.material.color = new Color(0.75f, 0.95f, 0.95f); //cyan
                break;


            case Type.cl6:
                name = "Type-O Star";
                transform.localScale *= StarManager.Instance.starClassSizes[6];
                age = Random.Range(0, 2);
                renderer.material.color = new Color(0.33f, 0.64f, 0.89f); //blue
                break;
        }

        selectRing.renderer.material.color = renderer.material.color;
    }

    public void TurnOnRing(float _size)
    {
        selectRing.SetActive(true);
        selectRing.transform.localScale = new Vector3(_size * 0.3f, _size * 0.3f, _size * 0.3f);
    }

    public void TurnOffRing()
    {
        selectRing.SetActive(false);
        selectRing.transform.localScale = new Vector3(selectRingScale, selectRingScale, selectRingScale);
    }

    bool IsConnectionDrawn(Star _star)
    { 
        if (LineDrawer.all.Count > 0)
        { 
            foreach(LineDrawer _line in LineDrawer.all)
            {
                if (_line.target == _star.gameObject && _line.origin == gameObject ||
                    _line.target == gameObject && _line.origin == _star.gameObject)
                    return true;
            }
        }

        return false;
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
