using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine.UIElements;

public class RocketManager : MonoBehaviour
{
    private float xInput, zInput;

    private float[] fuel = {30f, 25f, 20f};
    
    private int nivel = 1;

    private int[] inicialAngle = {0,100, 150};

    private int speed;

    private int maxspeed = 10;

    private float previousSpeed;

    private float rotateSpeed;
    
    private float throatSpeed = 300f;

    private float altura ;
    
    private bool isNotPlaying;



    private Rigidbody rb;

    [SerializeField] private TextMeshProUGUI m_Speed;

    [SerializeField] private TextMeshProUGUI m_Elevation;

    [SerializeField] private TextMeshProUGUI m_Fuel;

    [SerializeField] private TextMeshProUGUI m_Maxspeed;

    [SerializeField] private TextMeshProUGUI m_Result;
    //[SerializeField] private TextMeshProUGUI m_Result;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isNotPlaying = true;
        //rb.position = new Vector3(0, 25, 0);
        rb = this.gameObject.GetComponent<Rigidbody>();
        Time.timeScale = 0.0f;
        rb.rotation = Quaternion.Euler(new Vector3(0,0,inicialAngle[nivel]));
        m_Result.text = "Press P to start level";
    }

    private void OnCollisionEnter(Collision col)
    {
        if ((previousSpeed > maxspeed) || (Mathf.Abs(rb.rotation.z) >20))
        {
            m_Result.color = new Color32(210, 25, 25, 255);
            m_Result.text = "Crash";
            isNotPlaying = true;

        }

        else
        {
            m_Result.color = new Color32(25, 210, 25, 255);
            m_Result.text = "Has Aterrat";
            isNotPlaying = true;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isNotPlaying == true)
        {
           
            if (Input.GetKey(KeyCode.P))
            {
                isNotPlaying = false;
                Time.timeScale = 1.0f;
            }
            
        }
        else
        {
            altura = rb.position.y;

            if (Input.GetKey(KeyCode.Space) && (fuel[nivel] > 0))
            {
                //rb.AddForce(Vector3.up * throatSpeed);
                rb.velocity= rb.velocity + transform.up * Time.deltaTime * 25;
          
                fuel[nivel] = fuel[nivel] - 5 * Time.deltaTime;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0,0,10)));
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(new Vector3(0, 0, -10)));
            }
            m_Elevation.text = "High: " + altura.ToString("F1");
            m_Speed.text = "speed: " + rb.velocity.magnitude.ToString("F1");
            m_Fuel.text = "Fuel: " + fuel[nivel].ToString("F1");
            m_Maxspeed.text = "Max Speed: " + maxspeed.ToString("F1");
            previousSpeed = rb.velocity.magnitude;


        }
    }
        
}
