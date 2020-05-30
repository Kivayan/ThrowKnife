using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [Header ("Simple Rotation")]
    [SerializeField] bool simpleRotation = false;
    [SerializeField] float simpleRotationSpeed = 60f;


    [Header ("Sinusoidal Rotation")]
    [SerializeField] bool sinusoidalRotation = false;
    [SerializeField] float sinBasSpeed = 50f;
    [SerializeField] float sinFrequency = 4f;
    [SerializeField] float sinMagintude = 40f;
    [SerializeField] float sinSpeed;

    float sinTime = 0f;
    



    void Start()
    {
        
    }


    void Update()
    {
        RotationModeSelector();
    }

    void SpinBasic()
    {
        transform.Rotate(Vector3.forward * simpleRotationSpeed * Time.deltaTime);
    }

    void SpinSinusoid()
    {
        sinTime += Time.deltaTime;
        sinSpeed = sinBasSpeed + Mathf.Sin(sinFrequency * sinTime) * sinMagintude;
        transform.Rotate(Vector3.forward * Time.deltaTime * sinSpeed);
    }

    void RotationModeSelector()
    {
        if(
            (simpleRotation && sinusoidalRotation ) 
            ||
            (!simpleRotation && !sinusoidalRotation ) 
        )
        {
            throw new System.Exception ("Select only one of Rotation Modes in " + gameObject.name);
        }

        if(simpleRotation)
        {
            SpinBasic();
        }
    
        if(sinusoidalRotation)
        {
            SpinSinusoid();
        }
    }
    



}
