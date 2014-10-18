using UnityEngine;
using System.Collections.Generic;

public class LineDrawer : MonoBehaviour {

    public static List<LineDrawer> all = new List<LineDrawer>();

    LineRenderer line;
    [HideInInspector] public GameObject target;
    [HideInInspector] public GameObject origin;

    void Awake()
    { 
        line = GetComponent<LineRenderer>();
        LineDrawer.all.Add(this);
    }

    public void Draw(GameObject _origin, GameObject _target)
    {
        origin = _origin;
        target = _target;
        line.material = new Material(Shader.Find("Particles/Additive"));
        line.SetColors(_origin.renderer.material.color, _target.renderer.material.color);
        line.SetPosition(0, origin.transform.position);
        line.SetPosition(1, target.transform.position);
    }

    public void Stop()
    {
        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
    }
}
