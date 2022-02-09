using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDetector : MonoBehaviour
{
    Vector2 fingerUp, fingerDown;
    public bool swipeMoving = false;

    HashSet<Vector2> touches = new HashSet<Vector2>();

    // Update is called once per frame
    void Update()
    {

        foreach (Touch touch in Input.touches)
        {

            if (touch.phase == TouchPhase.Moved)
            {

                touches.Add(touch.position);
            }

            if (touch.phase == TouchPhase.Ended)
            {
                foreach (Vector2 touchPos in touches)
                {
                    print(touchPos);
                }
            }

        }

    }

    void first()
    {
        Vector2 lastPoint;
        foreach (Vector2 point in touches)
        {

            lastPoint = point;
        }
    }
}
