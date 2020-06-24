using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControl : MonoBehaviour
{
    AudioSource audioPlayer;
    [SerializeField] AudioClip[] hitSound;
    [SerializeField] AudioClip throwSound;
    [SerializeField] AudioClip failSound;


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
        audioPlayer = GetComponent<AudioSource>();
    }



    void ThrowKnife()
    {
        rb.AddForce(Vector3.up * Thrust);
        PlaySound(throwSound);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Target")
         {
            atTarget = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            gameObject.transform.SetParent(collision.gameObject.transform);
            PlayRandomSound(hitSound);
        }

        else
        {
            StatTracker.ThrowFailed();
            PlaySound(failSound);
        }
    } 

    private void PlaySound(AudioClip clip)
    {
        audioPlayer.clip = clip;
        audioPlayer.Play();
    }

    private void PlayRandomSound(AudioClip[] clipList)
    {
        int i = Random.Range(0, clipList.Length);
        audioPlayer.clip = clipList[i];
        audioPlayer.Play();
    }
}
