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
    public float gravity
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


        age = Random.Range(1, 13);

        //Setup graphics?
    }
}
