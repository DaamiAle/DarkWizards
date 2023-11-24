using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] CursorType defaultCursor, handCursor;

    private void OnMouseEnter()
    {
        Cursor.SetCursor(handCursor.cursorTexture, handCursor.cursorHotspot, CursorMode.ForceSoftware);
    }
    private void OnMouseExit()
    {
        Cursor.SetCursor(defaultCursor.cursorTexture, defaultCursor.cursorHotspot, CursorMode.Auto);
    }
}
