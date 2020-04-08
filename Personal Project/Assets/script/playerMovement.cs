using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed = 3f;
    public Rigidbody2D rg;
    public Vector2 PlayerMove;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove.x = Input.GetAxisRaw("Horizontal");
        PlayerMove.y = Input.GetAxisRaw("Vertical");

        
    }
    private void FixedUpdate()
    {
        rg.MovePosition(rg.position + PlayerMove * speed * Time.fixedDeltaTime);
    }
}
