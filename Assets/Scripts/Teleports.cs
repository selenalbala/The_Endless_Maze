using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Teleports : MonoBehaviour
{
    public string sceneName;

    private bool playerInRange;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange){
            SceneManager.LoadScene(sceneName);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
