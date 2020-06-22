using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    
    private enum SpinType {constant, sinus}
    [Header ("Rotation Type Selector")]
    [SerializeField] SpinType spinType;


    [Header ("Simple Rotation")]
    [SerializeField] float simpleRotationSpeed = 60f;


    [Header ("Sinusoidal Rotation")]
    [SerializeField] float sinBasSpeed = 50f;
    [SerializeField] float sinFrequency = 4f;
    [SerializeField] float sinMagintude = 40f;
    [SerializeField] float sinSpeed;
    float sinTime = 0f;


    void Update()
    {
        SpinSelector();
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

    void SpinSelector()
    {
        switch (spinType)
        {
            case SpinType.constant: SpinBasic();
            break;

            case SpinType.sinus: SpinSinusoid();
            break;
        }
    }
    



}
