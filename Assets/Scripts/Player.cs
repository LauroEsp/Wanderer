using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool jumpKeyWasPressed;
    private Rigidbody2D rigidbodyComponent;
    private float horizontalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            jumpKeyWasPressed = true;
        }
    }

    private void FixedUpdate() 
    {
        rigidbodyComponent.velocity = new Vector2(horizontalInput * 7, rigidbodyComponent.velocity.y);
        
        if (jumpKeyWasPressed == true)
        {
            rigidbodyComponent.AddForce(Vector2.up * 14, ForceMode2D.Impulse);
            jumpKeyWasPressed = false;
        }
    }
}
