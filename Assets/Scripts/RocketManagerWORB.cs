using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;

public class RocketManagerWORB : MonoBehaviour
{
    private float xInput, zInput;

    private float[] fuel = {30f, 25f, 20f};

    private int nivel = 0;

    private float speed = 0;

    private int maxspeed = 10;

    private float previousSpeed;

    private float rotateSpeed;
    
    private float throatSpeed = 10f;

    private float altura ;

    private float gravetat = 5f;

    private float posicioY;

    private bool isNotPlaying = true;
    
    


    

    [SerializeField] private TextMeshProUGUI m_Speed;

    [SerializeField] private TextMeshProUGUI m_Elevation;

    [SerializeField] private TextMeshProUGUI m_Fuel;

    [SerializeField] private TextMeshProUGUI m_Maxspeed;

    [SerializeField] private TextMeshProUGUI m_Result;
    //[SerializeField] private TextMeshProUGUI m_Result;
    
    
    // Start is called before the first frame update
    void Start()
    {
        //rb = this.gameObject.GetComponent<Rigidbody>();
        posicioY = gameObject.transform.position.y;
        m_Result.text = "Press P to start level";
    }

    

    // Update is called once per frame
    void Update()
    {

        if (isNotPlaying == true)
        {
           
            if (Input.GetKey(KeyCode.P))
            {
                isNotPlaying = false;
            }
            
        }
        
        else
        {
            altura = transform.position.y;
            m_Result.text = "";
            if (posicioY > 2.50)
            {
                speed = speed - gravetat * Time.deltaTime;
                posicioY = posicioY  + (speed * Time.deltaTime);
                transform.position = new Vector3(0, posicioY, 0);
            
                if (Input.GetKey(KeyCode.Space) && (fuel[nivel] > 0))
                {
                    //rb.AddForce(Vector3.up * throatSpeed);
                    speed = speed + throatSpeed * Time.deltaTime;
                    fuel[nivel] = fuel[nivel] - 5 * Time.deltaTime;
           
                }
            
                posicioY = posicioY  + (speed * Time.deltaTime);
                transform.position = new Vector3(0, posicioY, 0);
            }
       
            else
            {
                if (previousSpeed > maxspeed)
                {
                    m_Result.color = new Color32(210, 25, 25, 255);
                    m_Result.text = "Crash. Press P to restart level";
                    isNotPlaying = true;
                }

                else
                {
                    m_Result.color = new Color32(25, 210, 25, 255);
                    m_Result.text = "Has Aterrat";
                }
        
            }

        }
        
        
        
        
        m_Elevation.text = "High: " + altura.ToString("F1");
        m_Speed.text = "speed: " + speed.ToString("F1");
        m_Fuel.text = "Fuel: " + fuel[nivel].ToString("F1");
        m_Maxspeed.text = "Max Speed: " + maxspeed.ToString("F1");
        previousSpeed = -speed;


    }
}