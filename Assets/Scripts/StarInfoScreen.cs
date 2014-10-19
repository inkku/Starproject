using UnityEngine;
using System.Collections;

public class StarInfoScreen : MonoBehaviour {

    public float windowWidth;
    public Vector2 padding;
    public Vector2 margin;


    Star hoverStar;
    Rect windowRect;
    Rect nameRect;
    float nameHeight = 20;
    Rect ageRect;
    Rect gravityRect;


    void Awake()
    {
        windowRect = new Rect
            (
                Screen.width - (windowWidth + padding.x),
                Screen.height - ((nameHeight * 3) + padding.y + margin.y + 50),
                windowWidth,
                (nameHeight * 3) + (padding.y * 3) + margin.y
            );

        nameRect = new Rect
            (
                windowRect.x + padding.x,
                windowRect.y + padding.y,
                windowRect.width,
                nameHeight
            );

        ageRect = new Rect
            (
                nameRect.x,
                nameRect.yMax + margin.y,
                windowRect.width,
                nameHeight
            );

        gravityRect = new Rect
            (
                nameRect.x,
                ageRect.yMax + margin.y,
                nameRect.width,
                nameRect.height
            );
    }

    void Update()
    {


        
        
    }

    void OnGUI()
    {
        if (GameHandler.Instance.MouseIsHoveringOver<Star>(out hoverStar))
        {
            GUI.Box(windowRect, "");
            GUI.Label(nameRect, hoverStar.name);
            GUI.Label(ageRect, (hoverStar.ageXten / 10).ToString("F2") + " billion years old");
            GUI.Label(gravityRect, hoverStar.range.ToString() + "G");
        }
    }
}