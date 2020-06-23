using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControl : MonoBehaviour
{


    public float Thrust = 1f;
    public Rigidbody2D rb;

    private bool atTarget = false;

    private void OnEnable() 
    {
        KnifeSpawner.onKnifeThrow += ThrowKnife;
    }

    private void OnDisable() {
        
        KnifeSpawner.onKnifeThrow -= ThrowKnife;
    }


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }



    void ThrowKnife()
    {
        rb.AddForce(Vector3.up * Thrust);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Target")
         {
            atTarget = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            gameObject.transform.SetParent(collision.gameObject.transform);
        }

        else
        {
            StatTracker.ThrowFailed();
        }
    } 
}
