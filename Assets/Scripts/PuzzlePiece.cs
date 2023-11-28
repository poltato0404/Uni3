using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    private bool _dragging;


    void update()
    {
        if (!_dragging) return;

        var mousePosition = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mousePosition;
    }
    void OnMouseDown()
    {
        _dragging = true;
    }
}
