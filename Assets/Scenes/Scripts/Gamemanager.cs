using UnityEngine;
using UnityEngine.InputSystem.Android;
using UnityEngine.Rendering.Universal;

public class Gamemanager : MonoBehaviour
{
    public P1Movement Player1;
    public P2Movement Player2;
    public Light2D GlobalLight;
    public float Timer = 5f;
   
    public bool Shootout;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Player1.LightsOn == true || Player2.LightsOn == true)
        {
            GlobalLight.intensity = 1f;
            Shootout = true;
        }
        if (Player1.LightsOn == false && Player2.LightsOn == false)
        {
            GlobalLight.intensity = 0.5f;
            Shootout = false;
        }
        //if (Shootout)
        //{
        //    Timer -= Time.deltaTime;
        //    if (Input.GetKeyDown(KeyCode.LeftShift))
        //    {
        //        shootbulletsp1();
        //    }
        //    if (Input.GetKeyDown(KeyCode.RightShift))
        //    {
        //        shootbulletsp2();
        //    }
        //}

        //public void shootbulletsp12()

        //    ()
        //{
        //    Instantiate(bullet, transform.position, Quaternion.identity);

    }

}

