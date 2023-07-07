using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject characterPrefab;
    private GameObject characterInstance;

    public int totalGems; // Total de gemas que se deben recolectar para la victoria
    public int collectedGems; // Gemas recolectadas hasta ahora
    public int maxLives; // Número máximo de vidas
    public int otherLives; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Comprobar si ya existe una instancia del personaje
            characterInstance = GameObject.FindGameObjectWithTag("Player");

            // Si no existe, crear una nueva instancia y marcarla como no destruible al cambiar de escena
            if (characterInstance == null)
            {
                characterInstance = Instantiate(characterPrefab);
                DontDestroyOnLoad(characterInstance);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CollectGem()
    {
        collectedGems++;

        if (collectedGems >= totalGems)
        {
            SceneManager.LoadScene("Victory");
        }
    }

    public void LoseLife()
    {
        otherLives--;

        if (otherLives <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
