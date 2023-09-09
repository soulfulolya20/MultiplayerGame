using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb1;
    private Rigidbody2D rb2;
    private Rigidbody2D rb3;
    private Rigidbody2D rb4;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    private Animator anim;

    // Start is called before the first frame update
    private void Start()
    {
        rb1 = p1.GetComponent<Rigidbody2D>();
        rb2 = p2.GetComponent<Rigidbody2D>();
        rb3 = p3.GetComponent<Rigidbody2D>();
        rb4 = p4.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        float dirX1 = Input.GetAxisRaw("Horizontal_p1");
        float dirX2 = Input.GetAxisRaw("Horizontal_p2");
        float dirX3 = Input.GetAxisRaw("Horizontal_p3");
        float dirX4 = Input.GetAxisRaw("Horizontal_p4");

        rb1.velocity = new Vector2(dirX1 * 7f, rb1.velocity.y);
        rb2.velocity = new Vector2(dirX2 * 7f, rb2.velocity.y);
        rb3.velocity = new Vector2(dirX3 * 7f, rb3.velocity.y);
        rb4.velocity = new Vector2(dirX4 * 7f, rb4.velocity.y);

        if (Input.GetButtonDown("Jump_p1"))
        {
            rb1.velocity = new Vector2(rb1.velocity.x, 14f);
        }

        if (Input.GetButtonDown("Jump_p2"))
        {
            rb2.velocity = new Vector2(rb2.velocity.x, 14f);
        }

        if (Input.GetButtonDown("Jump_p3"))
        {
            rb3.velocity = new Vector2(rb3.velocity.x, 14f);
        }

        if (Input.GetButtonDown("Jump_p4"))
        {
            rb4.velocity = new Vector2(rb4.velocity.x, 14f);
        }

        if (dirX1 > 0f) 
        {
            anim.SetBool("running1", true);
        }
        else if (dirX1 < 0f)
        {
            anim.SetBool("running1", true);
        }
        else
        {
            anim.SetBool("running1", false);
        }

        if (dirX2 > 0f)
        {
            anim.SetBool("running2", true);
        }
        else if (dirX2 < 0f)
        {
            anim.SetBool("running2", true);
        }
        else
        {
            anim.SetBool("running2", false);
        }

        if (dirX3 > 0f)
        {
            anim.SetBool("running3", true);
        }
        else if (dirX3 < 0f)
        {
            anim.SetBool("running3", true);
        }
        else
        {
            anim.SetBool("running3", false);
        }

        if (dirX4 > 0f)
        {
            anim.SetBool("running4", true);
        }
        else if (dirX4 < 0f)
        {
            anim.SetBool("running4", true);
        }
        else
        {
            anim.SetBool("running4", false);
        }


    }
}
