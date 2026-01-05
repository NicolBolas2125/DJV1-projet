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
    [SerializeField] private Collider[] colliders = new Collider[32];
    private bool _test;
    // Start is called before the first frame update
    void Awake()
    {
        accesPNJs = bouton.GetComponent<Button>();
    }

    private IEnumerator WaitTestKill()
    {
        _test = false;
        yield return new WaitForSeconds(1f);
        _test = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (_test)
        {
            StartCoroutine(WaitTestKill());
            int size = Physics.OverlapSphereNonAlloc(transform.position, 7.5f, colliders);
            int crewCount = 0;
            int impCount = 0;
            for (int i = 0; i < size; i = i + 1)
            {
                if (colliders[i].TryGetComponent<Movement>(out var pnj))
                {
                    if (accesPNJs.StatusPNJs[pnj.id] == 1)
                    {
                        crewCount += 1;
                    }
                    if (accesPNJs.StatusPNJs[pnj.id] == 2 && pnj._canKill)
                    {
                        impCount += 1;
                    }
                }
            }
            if (impCount >= crewCount)
            {
                for (int i = 0; i < size; i = i + 1)
                {
                    if (colliders[i].TryGetComponent<Movement>(out var pnj))
                    {
                        if (accesPNJs.StatusPNJs[pnj.id] == 1)
                        {
                             accesPNJs.StatusPNJs[pnj.id] = 0;
                             pnj.Mort();
                        }
                        if (accesPNJs.StatusPNJs[pnj.id] == 2 && pnj._canKill)
                        {
                            pnj.StartCoroutine(pnj.KillCooldown());
                        }
                    }
                }
            }
        }
    }
}