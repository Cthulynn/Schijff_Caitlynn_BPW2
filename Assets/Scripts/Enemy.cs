using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int healthPoints = 5;
    public float dieDelay = .5f;
    public GameObject deathShiek;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void GetHit(int damage)
    {
        Debug.Log("enemy has felt the pain of a projectile");
        healthPoints -= damage;
        audioSource.Play();
        if (healthPoints <= 0)
        {
            GameObject shriek = Instantiate(deathShiek, transform);
            shriek.transform.parent = null;
            Destroy(gameObject);
        }
    }

    private IEnumerator Die()
    {
        yield return new WaitForSeconds(dieDelay);
    }
}
