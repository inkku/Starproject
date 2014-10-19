using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Star : MonoBehaviour {

    public static bool pauseReach = false;

    public enum Type { cl0, cl1, cl2, cl3, cl4, cl5, cl6 }
    public Type type;
    public int typeNum;
	
    public List<Star> connections;
	public float ageXten;
	public bool reached;
    public bool unDiscovered = true;

	public float energy
	{	  
        get { return StarManager.Instance.starClassEnergies[typeNum]; }
    }
    public float range
    {
        get { return StarManager.Instance.starClassGravities[typeNum]; }
    }

    public GameObject dysonSphere;
    public GameObject selectRing;
    float selectRingScale;

    public enum State
    { 
        Connected,
        LocallyConnected,
        NotConnected,
        Dying
    }

    public State state = State.NotConnected;

    public List<Star> starsInReach;

    public bool current;

    void Start()
    {
        selectRingScale = selectRing.transform.localScale.x;
		connections = new List<Star>();

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
        if (unDiscovered)
        {
            renderer.enabled = false;
            selectRing.renderer.enabled = false;
            dysonSphere.renderer.enabled = false;
        }

        else
        {
            renderer.enabled = true;
            selectRing.renderer.enabled = true;
            dysonSphere.renderer.enabled = true;
        }


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

                if (unDiscovered)
                    Discover();

                if (GameHandler.Instance.currentStar == this)
                    TurnOnRing(2);
			else if (GameHandler.Instance.currentStar.starsInReach.Contains(this) || (GameHandler.Instance.previousStar == this))
				TurnOnRing(1);
                else
                    TurnOffRing();
                break;

            case State.LocallyConnected:
                dysonSphere.SetActive(true);

                if (unDiscovered)
                    Discover();

                if (GameHandler.Instance.currentStar.starsInReach.Contains(this))
                    TurnOnRing(1);
                else
                    TurnOffRing();
                break;

            case State.Dying:
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
                typeNum = 0;
                renderer.material.color = new Color(1, 0.160f, 0.1f); //red
                break;

            case Type.cl1:
                name = "Type-K Star";
                typeNum = 1;
                renderer.material.color = new Color(1, 0.5f, 0.1f); //orange
                break;
                
            case Type.cl2:
                name = "Type-G Star";
                typeNum = 2;
                renderer.material.color = new Color(1, 0.8f, 0.1f); //yellow
                break;

            case Type.cl3:
                name = "Type-F Star";
                typeNum = 3;
                renderer.material.color = Color.white;
                break;

            case Type.cl4:
                name = "Type-A Star";
                typeNum = 4;
                renderer.material.color = new Color(0.94f, 0.97f, 0.97f); //white-ish
                break;

            case Type.cl5:
                name = "Type-B Star";
                typeNum = 5;
                renderer.material.color = new Color(0.75f, 0.95f, 0.95f); //cyan
                break;


            case Type.cl6:
                name = "Type-O Star";
                typeNum = 6;
                renderer.material.color = new Color(0.33f, 0.64f, 0.89f); //blue
                break;
        }

        transform.localScale *= Random.Range(StarManager.Instance.starClassSizes[typeNum].x, StarManager.Instance.starClassSizes[typeNum].y);
        ageXten = Random.Range(StarManager.Instance.starClassLifespans[typeNum].x, StarManager.Instance.starClassLifespans[typeNum].y);
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

        float reachForce = 0;

        float connections = StarManager.Instance.TotalNumConnections(this);
        //		if (StarManager.Instance.sumConnections(this) > 0) {
        //			reachForce = StarManager.Instance.sumEnergy (this) / StarManager.Instance.sumConnections (this);
        //		}

        ////Debug.Log("ReachForce: " + reachForce);

        var _hitColliders = Physics.OverlapSphere(transform.position, range);
        for (var i = 0; i < _hitColliders.Length; i++)
        {
            GameObject _hit = _hitColliders[i].gameObject;

            if ((_hit != gameObject && _hit.GetComponent<Star>()))
            {
                Star _star = _hit.GetComponent<Star>();
                starsInReach.Add(_star);


                if (_star.unDiscovered)
                {
                    _star.Discover();
                }
            }
        }

        if (starsInReach.Contains(GameHandler.Instance.previousStar) == false)
            starsInReach.Add(GameHandler.Instance.previousStar);

        return starsInReach;
    }

    //public bool IsInReach(Star _star)
    //{
    //    GetReach();
    //    return starsInReach.Contains(_star);
    //}

    public void Discover()
    {
        unDiscovered = false;
        StarManager.Instance.discoveredStars.Add(this);
    }

    public IEnumerator Die(float _time)
    {
        state = State.Dying;
        StarManager.Instance.allStars.Remove(this);
        StarManager.Instance.discoveredStars.Remove(this);

        float _timer = 0;
        bool _lock = false;

        while(_timer < _time)
        {
            transform.localScale *= (1 + Time.deltaTime);

            iTween.ColorTo(gameObject, Color.white, _time);
            iTween.ShakePosition(gameObject, new Vector3(0.1f, 0.1f, 0.1f), Time.deltaTime);

            if (_timer >= _time / 2 && _lock == false)
            {
                _lock = true;
                ParticleSystem _pSys1 = Instantiate(StarManager.Instance.fx_Death1, transform.position, Quaternion.identity) as ParticleSystem;
                ParticleSystem _pSys2 = Instantiate(StarManager.Instance.fx_Death2, transform.position, Quaternion.identity) as ParticleSystem;

                _pSys1.Play();
                _pSys2.Play();
            }

            yield return null;
           _timer += Time.deltaTime;
        }

        UnloadConnections();

        if (GameHandler.Instance.currentStar == this)
            GameHandler.Instance.state = GameHandler.GameState.Dead;

        Destroy(gameObject);
    }

    void UnloadConnections()
    { 
        foreach(Star _star in connections)
        {
            _star.connections.Remove(this);
        }

        connections.Clear();

        for (int i = 0; i < LineDrawer.all.Count; i++)
        {
            if (LineDrawer.all[i].target == gameObject || LineDrawer.all[i].origin == gameObject)
            {
                LineDrawer.all[i].Stop();
                Destroy(LineDrawer.all[i]);
                LineDrawer.all.RemoveAt(i);
                i--;
            }
        }
    }
}
