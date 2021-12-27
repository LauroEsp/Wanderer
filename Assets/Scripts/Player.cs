using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyWasPressed;
    private Rigidbody2D rigidbodyComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            jumpKeyWasPressed = true;
        }
    }

    private void FixedUpdate() 
    {
        if (jumpKeyWasPressed == true)
        {
            rigidbodyComponent.AddForce(Vector2.up * 13, ForceMode2D.Impulse);
            jumpKeyWasPressed = false;
        }
    }
}
