using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour {

    public float maxSpeed = 3;
    public float speed = 50f;
    public float jumpPower = 150f;

    public bool grounded;

    public int curHealth;
    public int maxHealth = 4;

    // Panel
    public GameObject Intro;
    public GameObject Intro2;
    public GameObject End;

    public GameObject currentUI;
    public List<GameObject> uiPanelList = new List<GameObject>();

    private Rigidbody2D rb2d;
    private Animator anim;
  


    // Use this for initialization
    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        curHealth = maxHealth;
        // Panel
        uiPanelList.Add(Intro);
        uiPanelList.Add(Intro2);
        uiPanelList.Add(End);

        Intro.SetActive(true);
        currentUI = Intro;
        currentUI.SetActive(true);
    }

    
        // Update is called once per frame
        void Update () {

        anim.SetBool("Grounded", grounded);
        anim.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));

        //om de sprite te flippen
        if (Input.GetAxis("Horizontal") < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        //voor springen
        if(Input.GetButtonDown("Jump") && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
        }

        //Health
        if(curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }
        if (curHealth <= 0)
        {
            Die(); 
        }

        //Game sluiten
        
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
            
        }

    }
    void FixedUpdate()
    {
        Vector3 easeVelocity = rb2d.velocity;
        easeVelocity.y = rb2d.velocity.y;
        //Kijk hier nog effe naar
        easeVelocity.z = 0.0f;
        easeVelocity.x *= 0.75f; //easeVelocity.x = 0.75f;

        float h = Input.GetAxis("Horizontal");

        //zorgen dat de player niet wegglijd
        if (grounded)
        {
            rb2d.velocity = easeVelocity;
        }

        //Player bewegen
        rb2d.AddForce((Vector2.right * speed) * h);

        //Zorgen dat de Player niet te snel gaat
        if (rb2d.velocity.x > maxSpeed)
        {
            rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
        }

        if (rb2d.velocity.x < -maxSpeed)
        {
           rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
        }

        //dit stukje zorgt ervoor dat de pannels aan/uitgaan wanneer je linkermuisknop klikt
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            //Debug.Log(currentUI);
            if (currentUI == Intro)
            {
                Intro.SetActive(false);
                currentUI = Intro2;
                currentUI.SetActive(true);
            }

            else if (currentUI == End)
            {
                End.SetActive(false);
            }
            else
            {
                foreach (GameObject panel in uiPanelList)
                {
                    panel.SetActive(false);
                }
                currentUI = null;
            }
        }
    }
    void Die()
    {
        SceneManager.LoadScene(1);
    }

    public void Damage(int dmg)
    {
        curHealth -= dmg;
    }

    public void TurnOnPanel()
    {
        End.SetActive(true);
    }

}
