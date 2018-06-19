using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 0;
    public int damage = 1;
    public LayerMask whatToHit;

    public float timeToFire = 0;
    public Transform firePoint;
    public GameObject projectilePrefab;

    private Animator anim;
    private AudioSource audioSource;

    //
    void Awake()
    {
        anim = gameObject.GetComponent<Animator>();
        audioSource = gameObject.GetComponent<AudioSource>();

        firePoint = transform.Find("FirePoint");
        if (firePoint == null)
        {
            Debug.LogError("No firePoint? WHAT?!");
        }
    }

    //Shoots
    void Update()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        //trigger animation here
        anim.SetTrigger("Attack");
        audioSource.Play();

        //Fireball beweegt
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 100, whatToHit);
        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);
        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit " + hit.collider.name + " and did " + damage + " damage. ");
            if (hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().GetHit(damage);
            }
        }

        float rotation = Vector3.Angle(transform.right, mousePosition - firePointPosition);
        if (mousePosition.y < firePointPosition.y)
        {
            rotation *= -1;
        }

        Transform projectile = Instantiate(projectilePrefab, firePoint).transform;
        projectile.GetComponent<Projectile>().zRotation = rotation;
        projectile.parent = null;
    }
}