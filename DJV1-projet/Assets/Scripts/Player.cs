using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float up = 0f;
    private float right = 0f;
    private float speed = 5f;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W))
        {
            up = 1f;
        }
        else if(Input.GetKey(KeyCode.S))
        {
            up = -1f;
        }
        else
        {
            up = 0f;
        }

        if(Input.GetKey(KeyCode.D))
        {
            right = 1f;
        }
        else if(Input.GetKey(KeyCode.A))
        {
            right = -1f;
        }
        else
        {
            right = 0f;
        }

        UnityEngine.Vector3 movement = new UnityEngine.Vector3(right, 0f, up);
        transform.position = transform.position + Time.deltaTime * speed * movement;
    }
}
