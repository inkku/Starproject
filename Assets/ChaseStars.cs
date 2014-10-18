using UnityEngine;
using System.Collections;

public class ChaseStars : MonoBehaviour {

    Transform target;

    void Start()
    {
        StartCoroutine(Chase());
    }

    IEnumerator Chase()
    {
        for (; ; )
        {
            if (GameHandler.Instance.currentStar)
            {
                target = GameHandler.Instance.currentStar.transform;
                transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime);
            }

            yield return null;
        }
    }
}
