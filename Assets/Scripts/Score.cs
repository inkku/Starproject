using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    void OnGUI()
    {
        GUI.Label(new Rect((Screen.width / 2) - 50, 0, 100, 20), GameHandler.Score.ToString() + " / " + GameHandler.ScoreMax.ToString());
    }
}
