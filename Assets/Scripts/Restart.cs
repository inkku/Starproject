using UnityEngine;
using System.Collections;

public class Restart : MonoBehaviour {

    bool pushed = false;
    bool pushedTwice = false;
    bool timerStarted = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            pushed = true;
    }

    void OnGUI()
    {
        if (pushed && pushedTwice == false)
        {
            GUI.Label(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 50, 200, 100), "--Press Escape again to restart--");

            if (timerStarted == false)
                StartCoroutine(Timer(3));
        }
    }

    IEnumerator Timer(float _time)
    {
        timerStarted = true;
        float _timer = 0;

        while (_timer < _time)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                pushedTwice = true;

            yield return null;
            _timer += Time.deltaTime;
        }

        if (pushedTwice)
            Application.LoadLevel(1);

        else
        {
            pushed = false;
            timerStarted = false;
        }
    }
}
