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
    [SerializeField] private bool[] present = new bool[16];
    [SerializeField] private int crewCount = 0;
    [SerializeField] private int impCount = 0;
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
            present[pnj.id] = true;
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Movement>(out var pnj)){
            present[pnj.id] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_test)
        {
            impCount = 0;
            crewCount = 0;
            for (int i = 0; i < 16; i++)
            {
                if (present[i])
                {
                    if (accesPNJs.StatusPNJs[i] == 1)
                    {
                        crewCount += 1;
                    }
                    if (accesPNJs.StatusPNJs[i] == 2)
                    {
                        impCount += 1;
                    }
                }
            }
            StartCoroutine(WaitTestKill());
            if (crewCount <= impCount && crewCount != 0 && Vector3.Distance(player.transform.position, transform.position) >= 10f)
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
                                accesPNJs.PNJs[i].GetComponent<Movement>().Kill();
                            }
                        }
                    }
                }
            }
        }
    }
}