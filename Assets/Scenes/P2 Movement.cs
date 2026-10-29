using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEditor.Experimental.GraphView.GraphView;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class P2Movement : MonoBehaviour
{
    public Rigidbody2D p2rb;
    public SpriteRenderer p2sr;
    public BoxCollider2D p2col;
    public Transform flashlight2;
    public float smoothness = 0.0025f;
    public float speed = 5f;
    public Vector2 desired;
    public static P2Movement _instance;
    public float radius = 6.5f;

    public bool LightsOn;
    P1Movement Player1;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _instance = this;
    }
    void Start()
    {
        p2col = GetComponent<BoxCollider2D>();
        p2rb = GetComponent<Rigidbody2D>();
        p2sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        EasedDirection();
        RaycastAngle(transform.position, p2rb.linearVelocity);
    }
    void EasedDirection()
    {
        desired = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Debug.Log("up");
            desired.y = speed;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Debug.Log("down");
            desired.y = -speed;

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Debug.Log("Right");
            desired.x = speed;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Debug.Log("left");

            desired.x = -speed;
        }
        if (desired == Vector2.zero && p2rb.linearVelocity != Vector2.zero)
        {
            desired = -p2rb.linearVelocity * .5f;
        }

        Vector2 direction = desired.normalized;
        Vector2 vel = Vector2.Lerp(p2rb.linearVelocity, desired, smoothness);
        p2rb.linearVelocity = vel;

        //Vector3 directionangle = new Vector3 (Quaternion.AngleAxis(45f, flashlight2.up));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, vel.normalized, radius);
        


        Debug.DrawRay(transform.position, vel.normalized * radius, Color.red);
        
       
        flashlight2.up = vel.normalized;
    }
    public void RaycastAngle(Vector2 Playerpt, Vector2 PlayerDirection)
    {
        float angle45 = ((2 * radius) * Mathf.PI) * (45 / 360);
        //Quaternion rotation = Quaternion.AngleAxis(angle45, new Vector3(0,0,PlayerDirection));
        Vector3 angle1 = Quaternion.Euler(0, 0, 13.75f) * PlayerDirection;
        Vector3 angle2 = Quaternion.Euler(0, 0, -13.75f) * PlayerDirection;
        Vector3 angle3 = Quaternion.Euler(0, 0, 6.875f) * PlayerDirection;
        Vector3 angle4 = Quaternion.Euler(0, 0, -6.875f) * PlayerDirection;
        RaycastHit2D hitangle1 = Physics2D.Raycast(transform.position, angle1.normalized, radius);
        RaycastHit2D hitangle2 = Physics2D.Raycast(transform.position, angle2.normalized, radius);
        RaycastHit2D hitangle3 = Physics2D.Raycast(transform.position, angle3.normalized, radius);
        RaycastHit2D hitangle4 = Physics2D.Raycast(transform.position, angle4.normalized, radius);

        
        Debug.DrawRay(transform.position, angle1.normalized * radius, Color.green);
        Debug.DrawRay(transform.position, angle2.normalized * radius, Color.green);
        Debug.DrawRay(transform.position, angle3.normalized * radius, Color.green);
        Debug.DrawRay(transform.position, angle4.normalized * radius, Color.green);

        if (hitangle1 || hitangle2 || hitangle3 || hitangle4 != Player1.gameObject)
        {
            return;
        }
        else
        {
            LightsOn = true;
        }

    } 
     
         
    
       
            

        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        //flashlight2.localRotation = quaternion.Euler(0, 0, angle);
}


