using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UIElements;

public class P1Movement : MonoBehaviour
{
    public Rigidbody2D p1rb;
    public SpriteRenderer p1sr;
    public BoxCollider2D p1col;
    public Transform flashlight;
    
    public float smoothness = 0.075f;
    public float speed = 5f;
    public Vector2 desired;
    public bool isfacingright;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        p1col = GetComponent<BoxCollider2D>();
        p1rb = GetComponent<Rigidbody2D>();
        p1sr = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        EasedDirection();
    }
    void EasedDirection()
    {
        desired = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("up");
            desired.y = speed;

        }
        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("down");
            desired.y = -speed;

        }
        if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Right");
            desired.x = speed;

        }
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("left");

            desired.x = -speed;
        }
        Vector2 direction = desired.normalized;
        if (desired == Vector2.zero && p1rb.linearVelocity != Vector2.zero)
        {
            desired = -p1rb.linearVelocity * .5f;
        }
        
        Vector2 vel = Vector2.Lerp(p1rb.linearVelocity, desired, smoothness);
        p1rb.linearVelocity = vel;
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vel.normalized, 6.5f);
        Debug.DrawRay(transform.position, vel.normalized * 6.5f, Color.red);
        flashlight.up = vel.normalized;

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 45f;
        //flashlight.localRotation  = Quaternion.Euler(0, 0, angle);
        //Debug.DrawRay(flashlight.position, flashlight.localRotation.eulerAngles, Color.green);
    }

}
