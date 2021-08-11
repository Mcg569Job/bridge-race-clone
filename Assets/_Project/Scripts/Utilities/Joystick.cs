using UnityEngine;


public class Joystick : MonoBehaviour
{
    public float Horizontal = 0;
    public float Vertical = 0;

    private Vector2 _startPos;
    private Vector2 _endPos;

    private float _minDistance = 40;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _startPos = (Vector2)Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            _endPos = (Vector2)Input.mousePosition;
            if (Vector2.Distance(_endPos, _startPos) > _minDistance)
            {

                Vector2 position = (_endPos - _startPos).normalized;

                Horizontal = position.x;
                Vertical = position.y;

                _startPos = (Vector2)Input.mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            Horizontal = 0;
            Vertical = 0;
        }
    }

}