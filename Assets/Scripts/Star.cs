using UnityEngine;
using System.Collections.Generic;

public class Star : MonoBehaviour {

    public enum Type { cl1, cl2, cl3, cl4, cl5, cl6, cl7, cl8, cl9, cl10 }
    public Type type;

    public List<Star> connections;
    public float age;
    //public float energy
    //{
    //    //get 
    //    //{
    //    //    switch (type)
    //    //    {
    //    //        case Type.cl1:
    //    //            return StarManager.energy_cl1;

    //    //        case Type.cl2:
    //    //            return StarManager.energy_cl2;

    //    //        case Type.cl3:
    //    //            return StarManager.energy_cl3;

    //    //        case Type.cl4:
    //    //            return StarManager.energy_cl4;

    //    //        case Type.cl5:
    //    //            return StarManager.energy_cl5;

    //    //        case Type.cl6:
    //    //            return StarManager.energy_cl6;

    //    //        case Type.cl7:
    //    //            return StarManager.energy_cl7;

    //    //        case Type.cl8:
    //    //            return StarManager.energy_cl8;

    //    //        case Type.cl9:
    //    //            return StarManager.energy_cl9;

    //    //        case Type.cl10:
    //    //            return StarManager.energy_cl10;
    //    //    }
    //    //}
    //}
    //public float gravity
    //{ 
    //    //get 
    //    //{
    //    //    switch (type)
    //    //    {
    //    //        case Type.cl1:
    //    //            return StarManager.gravity_cl1;

    //    //        case Type.cl2:
    //    //            return StarManager.gravity_cl2;

    //    //        case Type.cl3:
    //    //            return StarManager.gravity_cl3;

    //    //        case Type.cl4:
    //    //            return StarManager.gravity_cl4;

    //    //        case Type.cl5:
    //    //            return StarManager.gravity_cl5;

    //    //        case Type.cl6:
    //    //            return StarManager.gravity_cl6;

    //    //        case Type.cl7:
    //    //            return StarManager.gravity_cl7;

    //    //        case Type.cl8:
    //    //            return StarManager.gravity_cl8;

    //    //        case Type.cl9:
    //    //            return StarManager.gravity_cl9;

    //    //        case Type.cl10:
    //    //            return StarManager.gravity_cl10;
    //    //    }
    //    //}
    //}

    void Awake()
    {
        Setup();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Setup()
    {
        //int _rand = Random.Range(0, 101);

        //     if (_rand <= StarManager.probability_cl1) type = Type.cl1;
        //else if (_rand <= StarManager.probability_cl2) type = Type.cl2;
        //else if (_rand <= StarManager.probability_cl3) type = Type.cl3;
        //else if (_rand <= StarManager.probability_cl4) type = Type.cl4;
        //else if (_rand <= StarManager.probability_cl5) type = Type.cl5;
        //else if (_rand <= StarManager.probability_cl6) type = Type.cl6;
        //else if (_rand <= StarManager.probability_cl7) type = Type.cl7;
        //else if (_rand <= StarManager.probability_cl8) type = Type.cl8;
        //else if (_rand <= StarManager.probability_cl9) type = Type.cl9;
        //else if (_rand <= StarManager.probability_cl10) type = Type.cl10;


        age = Random.Range(1, 13);

        //Setup graphics?
    }
}
