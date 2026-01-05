using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public bool _meeting;
    public int id;
    public bool _canKill;


    public void Mort()
    {
        Debug.Log("Death");
    }



    public IEnumerator KillCooldown()
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
