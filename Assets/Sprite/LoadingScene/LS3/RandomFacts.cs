using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomFacts : MonoBehaviour
{

    public TextMeshProUGUI displayrandomfacts;


    /*
     public void BtnAction()
    {
        RandomList(3);
    }
     private void RandomList(int maxInt)
    {
        int randomNum = Random.Range(1, maxInt + 1);
        string fact;
        switch (randomNum)
        {
            case 1:
                fact = "With less than 10 nests per year, the hawksbill also can be " +
                    "recognised by their stunning carapace and hawk-like beak.";
                break;

            case 2:
                fact = "A typical Hawksbill Turtle weighs 40 to 90 kg " +
                    "and can lay 70 to 160 eggs at a time";
                break;

            case 3:
                fact = "Sweetlips is frequently can be found in reef caves and crevices. " +
                    "And its distinctive trait is its thick, rubbery lips. ";
                break;
            default:
                fact = "Error";
                break;

            return fact;

        }

        randomfacts.text = fact;
    }
     */

    /*
     private void RandomList()
    {
        string[] fact = new string[]
        {
            "With less than 10 nests per year, the hawksbill also can be recognised by their stunning carapace and hawk-like beak.",
            "Sweetlips is frequently can be found in reef caves and crevices. And its distinctive trait is its thick, rubbery lips.",
            "A typical Hawksbill Turtle weighs 40 to 90 kg and can lay 70 to 160 eggs at a time"
        };

        string randomfact = fact[Random.Range(0, fact.Length)];
        displayrandomfacts.text = randomfact;

    }
     */

}
