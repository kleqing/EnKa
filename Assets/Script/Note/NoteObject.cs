using System;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canPressed;
    public KeyCode keyToPress;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Button")
        {
            canPressed = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Button")
        {
            canPressed = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(keyToPress))
        {
            if (canPressed)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
