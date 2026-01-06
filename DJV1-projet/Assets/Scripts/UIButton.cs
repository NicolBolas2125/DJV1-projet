using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [Header("UI Vote")]
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;
    
    [Header("Accès aux variables du jeu")]
    [SerializeField] Game game;

    void Awake()
    {
        resultText.text = "Veuillez entrer le numéro du membre d'équipage que vous souhaitez voter (de 0 à 15)";
        resultText.color = Color.black;
    }

    public void vote()
    {
        string input = inputField.text;
        resultText.text = "Veuillez entrer le numéro du membre d'équipage que vous souhaitez voter (de 0 à 15)";

        if (int.TryParse(input, out var number))
        {
            if (number < 0 || number > 15)
            {
                resultText.text = "Cet ID n'existe pas.";
            }
            else
            {
                if (game.StatusPNJs[number] == 0)
                {
                    resultText.text = "Ce membre d'équipage est déjà mort";
                }
                else
                {
                    game.PNJs[number].GetComponent<Movement>().Mort();
                    game.StatusPNJs[number] = 0;
                    game.ui.SetActive(false);
                    game.endMeeting();
                }
            }
        }
    }
}
