using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour {

    bool dead = false;
    bool timerStarted = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (GameHandler.Instance.state == GameHandler.GameState.Dead)
            dead = true;
	}

    void OnGUI()
    {
        if(dead)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 100), "--Game Over--");

            if (timerStarted == false)
                StartCoroutine(Timer(3));
        }
    }

    IEnumerator Timer(float _time)
    {
        timerStarted = true;
        float _timer = 0;

        while(_timer < _time)
        {
            yield return null;
            _timer += Time.deltaTime;
        }

        Application.LoadLevel(0);
    }
}
