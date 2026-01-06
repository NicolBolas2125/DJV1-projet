using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINamePNJ : MonoBehaviour
{
    private Transform _camera;

    void Awake()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_camera);
        transform.Rotate(new Vector3(0,180,0));
    }
}
