using System.Collections;
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
    public float radius = 6.5f;
    public bool LightsOn;
    public P2Movement Player2;
    
    public static P1Movement _instance;

    public LayerMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _instance = this;
    }

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
        RaycastAngle(p1rb.linearVelocity);
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

        RaycastHit2D hit = Physics2D.Raycast(transform.position, vel.normalized, radius);
        Debug.DrawRay(transform.position, vel.normalized * radius, Color.red);
        flashlight.up = vel.normalized; 
    }
    public void RaycastAngle(Vector2 PlayerDirection)
    {
        float angle45 = ((2 * radius) * Mathf.PI) * (45 / 360);
        //Quaternion rotation = Quaternion.AngleAxis(angle45, new Vector3(0,0,PlayerDirection));
        for(int i = 0; i < 2; i++)
        {
            Vector3 angle = Quaternion.Euler(0, 0, 6.875f * (i + 1)) * PlayerDirection;
            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, angle.normalized, radius, mask);
            Debug.DrawRay(transform.position, angle.normalized * radius, Color.purple);
            if (hitinfo)
            {

                Debug.Log(hitinfo.collider.gameObject);
                if (hitinfo.collider.tag == "P2")
                {
                    LightsOn = true;
                }

            }
            
        }
        for(int i = 0; i < 2; i++)
        {
            Vector3 angle = Quaternion.Euler(0, 0, -6.875f * (i + 1)) * PlayerDirection;
            RaycastHit2D hitinfo = Physics2D.Raycast(transform.position, angle.normalized, radius, mask);
            Debug.DrawRay(transform.position, angle.normalized * radius, Color.purple);
            if (hitinfo)
            {

                Debug.Log(hitinfo.collider.gameObject);

                if (hitinfo.collider.tag == "P2")
                {
                    LightsOn = true;
                }

            }
        }
        if (LightsOn)
        {
            Debug.Log("Lights on");
        }


    }
    

}


