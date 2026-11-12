using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public Rigidbody2D PlayerRb;
    public SpriteRenderer PlayerSr;
    public BoxCollider2D PlayerCol;
    public Transform flashlight;

    public float smoothness = 0.075f;
    public float speed = 5f;
    public Vector2 desired;
    public bool isfacingright;
    public float radius = 6.5f;
    public bool LightsOn;
    public P2Movement Player2;
    public Gamemanager GM;
    public GameObject bullet;
    public Transform PlayerTransform;
    public float bulletSpeed = 10f;
    public static P1Movement _instance;

    public LayerMask mask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    void Awake()
    {
        PlayerCol = GetComponent<BoxCollider2D>();
        PlayerRb = GetComponent<Rigidbody2D>();
        PlayerSr = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerCol.name == "P1")
        {
            EasedDirectionwasd();
            RaycastAngle(PlayerRb.linearVelocity);
            if (GM.Shootout && Input.GetKeyDown(KeyCode.LeftShift))
            {
            
                shoot(transform);
            }
        }
        if (PlayerCol.name == "P2")
        {
            EasedDirectionArrows();
            RaycastAngle(PlayerRb.linearVelocity);
            if(GM.Shootout && Input.GetKeyDown(KeyCode.RightShift))
            {
                shoot(transform);
            }
        }
        
        
    }
    void EasedDirectionwasd()
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
        if (desired == Vector2.zero && PlayerRb.linearVelocity != Vector2.zero)
        {
            desired = -PlayerRb.linearVelocity * .5f;
        }

        Vector2 vel = Vector2.Lerp(PlayerRb.linearVelocity, desired, smoothness);
        PlayerRb.linearVelocity = vel;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, vel.normalized, radius);
        Debug.DrawRay(transform.position, vel.normalized * radius, Color.red);
        flashlight.up = vel.normalized;
    }
    void EasedDirectionArrows()
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
        Vector2 direction = desired.normalized;
        if (desired == Vector2.zero && PlayerRb.linearVelocity != Vector2.zero)
        {
            desired = -PlayerRb.linearVelocity * .5f;
        }

        Vector2 vel = Vector2.Lerp(PlayerRb.linearVelocity, desired, smoothness);
        PlayerRb.linearVelocity = vel;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, vel.normalized, radius);
        Debug.DrawRay(transform.position, vel.normalized * radius, Color.red);
        flashlight.up = vel.normalized;
    }
    public void RaycastAngle(Vector2 PlayerDirection)
    {
        float angle45 = ((2 * radius) * Mathf.PI) * (45 / 360);
        //Quaternion rotation = Quaternion.AngleAxis(angle45, new Vector3(0,0,PlayerDirection));
        for (int i = 0; i < 2; i++)
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
                if(hitinfo.collider.tag == "P1")
                {
                    LightsOn = true;
                }

            }

        }
        for (int i = 0; i < 2; i++)
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
                if(hitinfo.collider.tag == "P1")
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
    void shoot(Transform PlayerTransform)
    {
        GameObject newBullet = Instantiate(bullet, PlayerTransform.position, Quaternion.identity);
        Rigidbody2D bulletRb = newBullet.GetComponent<Rigidbody2D>();
        bulletRb.linearVelocity = PlayerTransform.up * bulletSpeed;
    }

}
    // Update is called once per frame
    
