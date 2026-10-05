using Unity.Mathematics;
using UnityEngine;

public class P2Movement : MonoBehaviour
{
    public Rigidbody2D p2rb;
    public SpriteRenderer p2sr;
    public BoxCollider2D p2col;
    public Transform flashlight2;
    public float smoothness = 0.0025f;
    public float speed = 5f;
    public Vector2 desired;
    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    }
    void EasedDirection()
    {
        desired = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Debug.Log("up");
            desired.y = speed;

        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            Debug.Log("down");
            desired.y = -speed;

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            Debug.Log("Right");
            desired.x = speed;

        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Debug.Log("left");

            desired.x = -speed;
        }
        if (desired == Vector2.zero && p2rb.linearVelocity != Vector2.zero)
        {
            desired = -p2rb.linearVelocity * .5f;
        }

        Vector2 direction = desired.normalized;
        Vector2 vel = Vector2.Lerp(p2rb.linearVelocity, desired, smoothness);
        p2rb.linearVelocity = vel;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vel.normalized, 6.5f);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        flashlight2.localRotation = quaternion.Euler(0, 0, angle);
    }

}
