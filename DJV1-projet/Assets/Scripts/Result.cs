using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{

    private IEnumerator SwitchMenu()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
    void Awake()
    {
        StartCoroutine(SwitchMenu());
    }
}
