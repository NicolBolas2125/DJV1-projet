using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    [SerializeField] private GameObject player;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Vector3 speed = player.transform.position - transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position, ref speed, Time.deltaTime);
    }
}