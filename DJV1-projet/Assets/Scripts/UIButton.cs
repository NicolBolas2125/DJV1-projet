using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;
    [SerializeField] Button bouton;

    void Awake()
    {
        resultText.text = "Veuillez entrer le nom du membre d'équipage que vous souhaitez voter";
        resultText.color = Color.black;
    }

    public void vote()
    {
        string input = inputField.text;
        resultText.text = "Veuillez entrer un numéro valide (de 0 à 15)";

        if (int.TryParse(input, out var number))
        {
            if (number < 0 || number > 15)
            {
                resultText.text = "Cet ID n'existe pas.";
            }
            else
            {
                if (bouton.StatusPNJs[number] == 0)
                {
                    resultText.text = "Ce membre d'équipage est déjà mort";
                }
                else
                {
                    bouton.PNJs[number].GetComponent<Movement>().Mort();
                    bouton.StatusPNJs[number] = 0;
                    bouton.ui.SetActive(false);
                    bouton.endMeeting();
                }
            }
        }
    }
}
