using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 50, 200, 100));

        if (GUILayout.Button("Play"))
            Application.LoadLevel(1);

        if (GUILayout.Button("Quit"))
            Application.Quit();

        GUILayout.EndArea();
    }
}
