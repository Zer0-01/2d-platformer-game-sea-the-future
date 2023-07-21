using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChange : MonoBehaviour
{

    [SerializeField] private string sceneName;

    //test
    [SerializeField] private GameObject playerObject;
    private PlayerController player;

   

    private void Start()
    {
        player = playerObject.GetComponent<PlayerController>();

     
    }

    

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && player.hasFullCherries())
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
