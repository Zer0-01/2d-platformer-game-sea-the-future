using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingSc : MonoBehaviour
{
    
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI displayrandomfacts;


    [SerializeField]
    private GameObject loading_Bar_Holder;

    [SerializeField]
    private Slider loading_Bar_Progress;

    [SerializeField] string levelName;

    private float progress_Value = 1.1f;
    public float progress_Multiplier_1 = 0.07f;
    public float progress_Multiplier_2 = 0.002f;
    public float load_Level_Time = 8;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadingSomeLevel());

        RandomList();
    }

    // Update is called once per frame
    void Update()
    {
        ShowLoadingScreen();

    }

    public void LoadLevel(string levelName)
    {
        loading_Bar_Holder.SetActive(true);

        progress_Value = 0f;
        //Time.timeScale = 0f;

        SceneManager.LoadScene(levelName);
    }

    void ShowLoadingScreen()
    {
        if (progress_Value < 1f)
        {
            progress_Value += progress_Multiplier_1 * progress_Multiplier_2;
            loading_Bar_Progress.value = progress_Value;

            if (progress_Value >= 1f)
            {
                progress_Value = 1.1f;

                loading_Bar_Progress.value = 0f;

                loading_Bar_Holder.SetActive(false);


                //Time.timeScale = 1f;
            }

            progressText.text = progress_Value * 100 + "%";


        }
    }

    IEnumerator LoadingSomeLevel()
    {
        yield return new WaitForSeconds(load_Level_Time);

        LoadLevel(levelName);
    }

    private void RandomList()
    {
        string[] fact = new string[]
        {
            "With less than 10 nests per year, the hawksbill also can be recognised by their stunning carapace and hawk-like beak.",
            "Sweetlips is frequently can be found in reef caves and crevices. And its distinctive trait is its thick, rubbery lips.",
            "A typical Hawksbill Turtle weighs 40 to 90 kg and can lay 70 to 160 eggs at a time",
            "Sweetlips are nocturnal animals that can reach a length of 72 cm. Juveniles have brown bodies with noticeable white spots.",
            "Usually, Green Turtle weight around 110 to 180kg and can produce 100 to 140 eggs once.",
            "A lot of Echinoderms can be found at Pulau Kapas such as Warty Sea Star, Feather Star, Purple and Red Cushion Seastar and Golden Sea Cucumber.",
            "‘Chagar Hutang Turtle Sanctuary’ is the main landing place for Green and Hawskbill turtle. It is one of the important nesting beaches in Redang Island.",
            "The most protected Green Turtle’s nests were found at 1731 in 2020, and during the peak season, more than 30 nesting activities took place each night.",
            "From March through September is when Chelonia mydas or Penyu Pulau lay their eggs.",
            "Penyu Karah or Penyu Sisik is scientifically called Eretmochelys imbricata.",
            "Penyu Karah's nesting seasons are between March until September.",
            "In 2015, Malaysia Book of Record declared that Terengganu has the longest and cleanest beach. From Pantai Kemasek to Pantai Kertih, around 11.3km.",
            "Malaysia has worked on the enforcement of the ban on the possession of all types of turtle eggs in 2021 to protect the eggs and its species [Turtle Enactment 1951 (Amendment 2021)].",
            "Pulau Kapas is one of the nearest islands to Terengganu just about 104km and 10 to 15 minutes from the mainland.",
            "The black-tip reef sharks are active, strong swimmers that travel alone or in small groups. They are frequently seen nearshore in shallow water coral reefs and the intertidal zone.",
            "Ghost nets are fishing nets that have been abandoned, lost at sea, or thrown away after becoming unusable.",
            "Ghost Net often being left tangled on a rocky reef or drifting in open sea, and are frequently almost unnoticeable in the low light.",
            "Ghost Nets are floating nets that catch variety of fish, turtles, seabirds, and aquatic animals as well as other nets, plastic, and organic waste."
            //18 facts
        };

        string randomfact = fact[Random.Range(0, fact.Length)];
        displayrandomfacts.text = randomfact;

    }
}
