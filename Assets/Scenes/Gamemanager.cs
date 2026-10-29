using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.Rendering.Universal;

public class Gamemanager : MonoBehaviour
{
    P1Movement Player1;
    P2Movement Player2;
    public Light2D GlobalLight;
    public float Timer = 5f;

    public bool Shootout;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Player1.LightsOn || Player2.LightsOn)
        //{
        //    GlobalLight.intensity = 1;
        //    Shootout = true;
        //}
        //if (Shootout)
        //{
        //   Timer -= Time.deltaTime;
        //  if(Input.GetKeyDown(KeyCode.LeftShift)
        //  {
        //    shootbulletsp1();
        //  }
        // if (Input.GetKeyDown(KeyCode.Enter)){
        //    shootbulletsp2();}
        //}

        //public void shootbulletsp1()
        //{
        //   instantiate(bullet, transform.position, Quaternion.identity);
        //   
        //}

    }
}
