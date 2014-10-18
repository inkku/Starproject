using UnityEngine;
using System.Collections;

public class StarInfoScreen : MonoBehaviour {

    public Vector2 position;
    public float windowWidth;
    public Vector2 padding;
    public Vector2 margin;


    Star selectedStar;
    Rect windowRect;
    Rect nameRect;
    float nameHeight = 10;
    Rect ageRect;
    Rect gravityRect;
    Rect gravityValRect;


    void Awake()
    {
        nameHeight /= Screen.height;

        windowRect = new Rect
            (
                position.x / Screen.width,
                position.y / Screen.height,
                windowWidth / Screen.width,
                (nameHeight * 2) + (padding.y * 2) + margin.y / Screen.height
            );

        nameRect = new Rect
            (
                windowRect.x + padding.x,
                windowRect.y + padding.y,
                windowRect.width * 0.75f - padding.x,
                nameHeight
            );

        ageRect = new Rect
            (
                windowRect.width * 0.75f,
                nameRect.y,
                windowRect.width * 0.25f,
                nameHeight
            );

        gravityRect = new Rect
            (
                nameRect.x,
                nameRect.xMax + margin.x,
                nameRect.width,
                nameRect.height
            );

        gravityValRect = new Rect
            (
                ageRect.x,
                gravityRect.y,
                ageRect.width,
                ageRect.height
            );
    }

    void Update()
    {
        if (GameHandler.Instance.currentStar)
            selectedStar = GameHandler.Instance.currentStar;
        else selectedStar = null;
    }

    void OnGUI()
    {
        if(selectedStar)
        {
            GUI.Box(windowRect, "");
            GUI.Label(nameRect, selectedStar.name);
            GUI.Label(ageRect, selectedStar.age.ToString());
            GUI.Label(gravityRect, "Gravity");
            GUI.Label(gravityValRect, selectedStar.range.ToString());
        }
    }
}
