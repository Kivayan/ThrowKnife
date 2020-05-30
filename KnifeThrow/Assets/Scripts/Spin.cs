using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    [SerializeField] float flatRotationSpeed = 50f;

    [SerializeField] float sinBasSpeed = 50f;
    [SerializeField] float sinFrequency = 4f;
    [SerializeField] float sinMagintude = 40f;
    [SerializeField] float sinSpeed;

    float sinTime = 0f;
    



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SpinSinusoid();
    }

    void SpinBasic()
    {
        transform.Rotate(Vector3.forward * flatRotationSpeed * Time.deltaTime);
    }

    void SpinSinusoid()
    {
        sinTime += Time.deltaTime;
        sinSpeed = sinBasSpeed + Mathf.Sin(sinFrequency * sinTime) * sinMagintude;
        transform.Rotate(Vector3.forward * Time.deltaTime * sinSpeed);
    }
}
