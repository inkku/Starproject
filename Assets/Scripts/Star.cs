using UnityEngine;
using System.Collections.Generic;

public class Star : MonoBehaviour {

    public enum Type { cl0, cl1, cl2, cl3, cl4, cl5, cl6, cl7, cl8, cl9 }
    public Type type;
	
    public List<Star> connections;
	public bool connected;
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

    void Awake()
    {
        Setup();
    }

    void Setup()
    {
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



	public bool inMyConnections (Star _newStar)
	{	
		bool ans = false;

		foreach (Star _star in connections)
		{
			if(_star == _newStar)
			{
				ans = true;
			}
		}

		return ans;
	}

	public void getStarsInReach ()
	{
		Vector3 center = this.transform.position;
		float radius = range;

		var hitColliders = Physics.OverlapSphere(center, radius);
		
		for (var i = 0; i < hitColliders.Length; i++) {
			//hitColliders[i].SendMessage("AddDamage");
		}	

	}

	void Update() 
 //* THIS IS NOT WORKING
		//because they dont have any connections currently

		//you wile have double conenctions so add something that makes you not add linerender on start and the conencted star but only one of them
	{
				
		foreach (Star _star in connections) 
		{
			LineRenderer line = this.gameObject.AddComponent<LineRenderer>();
			line.SetWidth(10, 10);
			line.SetVertexCount(2);
			line.material = this.gameObject.renderer.material;
			line.renderer.enabled = true;
			line.SetPosition(0, _star.gameObject.transform.position);
			line.SetPosition(1, this.gameObject.transform.position);
			
		}
	}

}
