using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private GameObject tombe;
    public bool _meeting;
    public int id;
    public bool _canKill;


    public void Mort()
    {
        Debug.Log("Death");
        Instantiate(tombe, transform.position,transform.rotation);
        gameObject.SetActive(false);
    }


    // Je sais pas pourquoi il ne veut pas me laisser appeler directement ma coroutine dans bouton donc je fais une fonction
    public void Kill()
    {
        StartCoroutine(KillCooldown());
    }
    private IEnumerator KillCooldown()
    {
        _canKill = false;
        yield return new WaitForSeconds(20f);
        _canKill = true;
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
