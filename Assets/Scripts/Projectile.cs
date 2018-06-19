using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float velocity = 5;
    public float zRotation = 0;
    public int suicideTime = 1;

    private Rigidbody2D rb;
	
	private void Start () {
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
        rb = gameObject.GetComponent<Rigidbody2D>();
        float xVelocity = Mathf.Cos(zRotation * .017453f) * velocity; // 0.017453 is pi/180, to convert euler to radian
        float yVelocity = Mathf.Sin(zRotation * .017453f) * velocity;
        rb.AddForce(new Vector2(xVelocity, yVelocity));

        StartCoroutine("Suicide");
	}

    private IEnumerator Suicide()
    {
        yield return new WaitForSeconds(suicideTime);
        Destroy(gameObject);
        yield return null;
    }
}
