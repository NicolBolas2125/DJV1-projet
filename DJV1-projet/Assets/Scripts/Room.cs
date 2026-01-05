using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bouton;
    private Button accesPNJs;
    private bool[] present = new bool[16];
    private int crewCount = 0;
    private int impCount = 0;
    [SerializeField] private Collider[] colliders = new Collider[32];
    private bool _test = true;
    // Start is called before the first frame update
    void Awake()
    {
        accesPNJs = bouton.GetComponent<Button>();
        for (int i = 0; i < 16; i++)
        {
            present[i] = false;
        }
    }

    private IEnumerator WaitTestKill()
    {
        _test = false;
        yield return new WaitForSeconds(1f);
        _test = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Movement>(out var pnj)){
            if (accesPNJs.StatusPNJs[pnj.id] == 1)
            {
                crewCount += 1;
            }
            else
            {
                if (accesPNJs.StatusPNJs[pnj.id] == 2)
                {
                    impCount += 1;
                }
            }
            present[pnj.id] = true;
            Debug.Log(pnj.id);
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Movement>(out var pnj)){
            if (accesPNJs.StatusPNJs[pnj.id] == 1)
            {
                crewCount -= 1;
            }
            else
            {
                if (accesPNJs.StatusPNJs[pnj.id] == 2)
                {
                    impCount -= 1;
                }
            }
            present[pnj.id] = false;
            Debug.Log(pnj.id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_test)
        {
            StartCoroutine(WaitTestKill());
            if (crewCount <= impCount && crewCount != 0)
            {
                for (int i = 0; i < 16; i++)
                {
                    if (present[i])
                    {
                        if (accesPNJs.StatusPNJs[i] == 1)
                        {
                            accesPNJs.PNJs[i].GetComponent<Movement>().Mort();
                            accesPNJs.StatusPNJs[i] = 0;
                            present[i] = false;
                            crewCount -= 1;
                        }
                        else
                        {
                            if (accesPNJs.StatusPNJs[i] == 2)
                            {
                                accesPNJs.GetComponent<Movement>().Kill();
                            }
                        }
                    }
                }
            }
        }
    }
}