using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    public TextMeshProUGUI progressText;
    public TextMeshProUGUI displayrandomfacts;


    [SerializeField]
    private GameObject loading_Bar_Holder;

    [SerializeField]
    private Slider loading_Bar_Progress;

    [SerializeField] string sceneName;

    private float progress_Value = 1.1f;
    public float progress_Multiplier_1 = 0.5f;
    public float progress_Multiplier_2 = 0.07f;

    public float load_Level_Time = 2;

    private void Awake()
    {
        MakeSingleton();
    }
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadingSomeLevel());

        RandomList();
    }

    // Update is called once per frame
    void Update()
    {
        ShowLoadingScreen();
    }

    void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
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

        LoadLevel(sceneName);
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
            "‘Chagar Hutang Turtle Sanctuary’ is the main landing place for Green and Hawskbill turtle. It is one of the important nesting beaches in Redang Island."
        };

        string randomfact = fact[Random.Range(0, fact.Length)];
        displayrandomfacts.text = randomfact;

    }
}
