using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Game : MonoBehaviour
{
    [Header("Elements de gestion du jeu")]
    [SerializeField] private GameObject player;

    //Permet de gérer les rôles (0 = mort, 1 = crewmate, 2 = imposteur)
    public int[] StatusPNJs = new int[16];
    //Liste des PNJs
    public GameObject[] PNJs = new GameObject[16];

    [SerializeField] private GameObject imp;
    [SerializeField] private GameObject crew;

    [Header("UI")]
    //Champ pour le vote
    [SerializeField] public GameObject ui;

    [Header("Permet de contrôler si on peut faire certaines actions")]
    private bool _canMeeting = true;
    public bool _canKill;

    private void startGame()
    {
        ui.SetActive(false);
        int imp1 = Random.Range(0,17);
        int imp2 = Random.Range(0,17);
        while (imp1 == imp2)
        {
            imp2 = Random.Range(0,17);
        }
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
                int id = 4*i + j;
                if (StatusPNJs[id] == 1)
                {
                    PNJs[id] = Instantiate(crew,transform.position,transform.rotation);
                    PNJs[id].transform.position = new Vector3(i-1, 1, j-1);
                    PNJs[id].GetComponent<Movement>().id = id;
                    PNJs[id].GetComponent<Movement>().namePNJ.text = id.ToString();
                }
                else
                {
                    PNJs[id] = Instantiate(imp,transform.position,transform.rotation);
                    PNJs[id].transform.position = new Vector3(i-1, 1, j-1);
                    PNJs[id].GetComponent<Movement>().id = id;
                    PNJs[id].GetComponent<Movement>().namePNJ.text = id.ToString();
                }
            }
        }
    }


    public void endGame()
    {
        int impCount = 0;
        int crewCount = 0;
        for (int i = 0; i< 16; i++)
        {
            if (StatusPNJs[i] == 1)
            {
                crewCount += 1;
            }
            if (StatusPNJs[i] == 2)
            {
                impCount += 1;
            }
        }
        if (impCount == 0)
        {
            SceneManager.LoadScene("Victory");
        }
        if (crewCount <= impCount)
        {
            SceneManager.LoadScene("Defeat");
        }
    }


    private void callMeeting()
    {
        for (int i=0; i < 4; i++)
        {
            for (int j=0; j < 4; j++)
            {
                int id = 4*i + j;
                if (StatusPNJs[4*i + j] != 0)
                {
                    PNJs[id].GetComponent<Movement>().agent.SetDestination(new Vector3(i-1,1,j-1));
                    PNJs[id].transform.position = new Vector3(i-1,1,j-1);
                    PNJs[id].GetComponent<Movement>()._meeting = true;
                }
            }
        }
        ui.SetActive(true);
    }

    

    public void endMeeting()
    {
        for (int i=0; i < 4; i++)
        {
            for (int j=0; j < 4; j++)
            {
                int id = 4*i+j;
                if (StatusPNJs[id] != 0)
                {
                    PNJs[id].GetComponent<Movement>().DecideTarget();
                    PNJs[id].GetComponent<Movement>()._meeting = false;
                }
            }
        }
        endGame();
        StartCoroutine(WaitMeeting());
    }



    private IEnumerator WaitMeeting()
    {
        _canMeeting = false;
        yield return new WaitForSeconds(15);
        _canMeeting = true;
    }

    void Start()
    {
        startGame();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _canMeeting)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= 3f)
            {
                callMeeting();
            }
        }
    }
}
