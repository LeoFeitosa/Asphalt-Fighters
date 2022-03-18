using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    Vector3 positionTouch;
    bool touchInput = false;
    bool touchInputMove = false;

    public float MovementsKeyboard()
    {
        if (!enabled)
        {
            return 0;
        }

        if (Input.anyKey)
        {
            touchInput = false;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");

        return horizontal;
    }

    public float MovementsTouch()
    {
        if (!enabled)
        {
            return 0;
        }

        if (Input.touchCount > 0)
        {
            touchInput = true;
            Touch touch = Input.GetTouch(0);
            positionTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.phase == TouchPhase.Moved)
            {
                touchInputMove = true;
            }
            else
            {
                touchInputMove = false;
            }
        }
        else
        {
            touchInputMove = false;
        }

        positionTouch.z = 0;
        return positionTouch.x;
    }

    public bool TouchOnTheMove()
    {
        return touchInputMove;
    }

    public bool IsTouch()
    {
        return touchInput;
    }
}