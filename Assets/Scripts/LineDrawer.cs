using UnityEngine;
using System.Collections;

public class LineDrawer : MonoBehaviour {

    LineRenderer line;
    [HideInInspector] public GameObject target;

    void Awake()
    { 
        line = GetComponent<LineRenderer>();
    }

    public void Draw(GameObject _target)
    {
        target = _target;
        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.transform.position);
    }

    public void Stop()
    {
        line.SetPosition(1, transform.position);
    }
}
