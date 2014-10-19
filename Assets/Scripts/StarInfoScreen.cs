using UnityEngine;
using System.Collections;

public class StarInfoScreen : MonoBehaviour {

    public float windowWidth;
    public Vector2 padding;
    public Vector2 margin;


    Star hoverStar;
    Star savedStar;
    Vector2 savedMousePos;


    Rect windowRect;
    Rect nameRect;
    float nameHeight = 20;
    Rect ageRect;
    Rect gravityRect;

    public float hoverDistance;


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
        if (Input.GetKey(KeyCode.Mouse1) && savedStar)
        {
            windowRect.position = new Vector2(savedMousePos.x + hoverDistance, savedMousePos.y + hoverDistance);
            nameRect.position = new Vector2(windowRect.x + padding.x, windowRect.y + padding.y);
            ageRect.position = new Vector2(nameRect.x, nameRect.yMax + margin.y);
            gravityRect.position = new Vector2(nameRect.x, ageRect.yMax + margin.y);

            GUI.Box(windowRect, "");
            GUI.Label(nameRect, savedStar.name);
            GUI.Label(ageRect, (savedStar.ageXten / 10).ToString("F2") + " billion years old");
            GUI.Label(gravityRect, savedStar.range.ToString() + " (gravity)");
        }

        else if (GameHandler.Instance.MouseIsHoveringOverStar(out hoverStar))
        {
            savedStar = hoverStar;
            savedMousePos = Event.current.mousePosition;

            windowRect.position = new Vector2(Event.current.mousePosition.x + hoverDistance, Event.current.mousePosition.y + hoverDistance);
            nameRect.position = new Vector2(windowRect.x + padding.x, windowRect.y + padding.y);
            ageRect.position = new Vector2(nameRect.x, nameRect.yMax + margin.y);
            gravityRect.position = new Vector2(nameRect.x, ageRect.yMax + margin.y);

            GUI.Box(windowRect, "");
            GUI.Label(nameRect, hoverStar.name);
            GUI.Label(ageRect, (hoverStar.ageXten / 10).ToString("F2") + " billion years old");
            GUI.Label(gravityRect, hoverStar.range.ToString() + " (gravity)");
        }
    }
}