using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;


public class Movement : MonoBehaviour
{
    [Header("Elements propres des personnages")]
    public int id;
    [SerializeField] public TextMeshProUGUI namePNJ;


    [Header("Navigation")]
    public NavMeshAgent agent;
    private Vector3 target;
    [SerializeField] private Vector3[] rooms = new Vector3[10];


    [Header("Gestion mort et désactivation actions")]
    [SerializeField] private GameObject tombe;
    public bool _meeting;




    public void Mort()
    {
        Instantiate(tombe, transform.position,transform.rotation);
        gameObject.SetActive(false);
    }



    public void DecideTarget()
    {
        int n = Random.Range(0,10);
        target = rooms[n];
        agent.SetDestination(target);
    }

    private IEnumerator InterestPointInRoom()
    {
        float timer = Random.Range(5f, 15f);
        yield return new WaitForSeconds(timer);
        DecideTarget();
    }


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        DecideTarget();
    }


    // Update is called once per frame
    void Update()
    {
        if (!_meeting)
        {
            if (Vector3.Distance(target, transform.position) <= 4f)
            {
                StartCoroutine(InterestPointInRoom());
            }
        }
    }
}
