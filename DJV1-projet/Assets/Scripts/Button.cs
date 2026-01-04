using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    private Collider[] colliders = new Collider[16];

    //Permet de gérer les rôles (0 = mort, 1 = crewmate, 2 = imposteur)
    public int[] StatusPNJs = new int[16];
    //Liste des PNJs
    public GameObject[] PNJs = new GameObject[16];

    [SerializeField] private GameObject imp;
    [SerializeField] private GameObject crew;

    private void startGame()
    {
        int imp1 = Random.Range(0,17);
        int imp2 = Random.Range(0,17);
        while (imp1 == imp2)
        {
            imp2 = Random.Range(0,17);
        }
        Debug.Log(imp1);
        for (int i = 0; i < 16; i++)
        {
            if (i == imp1 || i == imp2)
            {
                StatusPNJs[i] = 2;
            }
            else
            {
                StatusPNJs[i] = 1;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (StatusPNJs[4*i + j] == 1)
                {
                    PNJs[4*i + j] = Instantiate(crew,transform.position,transform.rotation);
                    PNJs[4*i + j].transform.position = new Vector3(i-1, 1, j-1);
                    PNJs[4*i + j].GetComponent<Movement>().id = 4* i + j;
                }
                else
                {
                    PNJs[4*i + j] = Instantiate(imp,transform.position,transform.rotation);
                    PNJs[4*i + j].transform.position = new Vector3(i-1, 1, j-1);
                    PNJs[4*i + j].GetComponent<Movement>().id = 4* i + j;
                }
            }
        }
    }




    private void callMeeting()
    {
        for (int i=0; i < 4; i++)
        {
            for (int j=0; j < 4; j++)
            {
                if (StatusPNJs[4*i + j] != 0)
                {
                    PNJs[4*i + j].transform.position = new Vector3(i-1,1,j-1);
                    PNJs[4*i + j].GetComponent<Movement>()._meeting = true;
                }
            }
        }
    }

    private void endMeeting()
    {
        for (int i=0; i < 4; i++)
        {
            for (int j=0; j < 4; j++)
            {
                if (StatusPNJs[4*i + j] != 0)
                {
                    PNJs[4*i + j].GetComponent<Movement>()._meeting = false;
                }
            }
        }
    }


    void Start()
    {
        startGame();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Scan");
            int size = Physics.OverlapSphereNonAlloc(transform.position, 4f, colliders);
            for (int i = 0; i < size; i = i + 1)
            {
                if (colliders[i].TryGetComponent<Player>(out var player))
                {
                    Debug.Log("Meeting");
                    callMeeting();
                }
            }
        }
    }
}
