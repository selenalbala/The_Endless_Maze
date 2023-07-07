using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    private Image[] heartImages;
    public Sprite fullHeartSprite;
    public Sprite emptyHeartSprite;

    private GameManager gameManager; // Referencia al GameManager

    private void Start()
    {
        currentLives = maxLives;
        FindHeartImages();
        UpdateHeartUI();

        // Obtener la referencia al GameManager
        gameManager = GameManager.instance;

        // Suscribirse al evento de cambio de escena
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void OnDestroy()
    {
        // Darse de baja del evento de cambio de escena
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    void FindHeartImages()
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            heartImages = canvas.GetComponentsInChildren<Image>();
        }
        else
        {
            Debug.LogError("No se encontró el objeto Canvas en la escena.");
        }
    }

    public void LoseLife()
    {
        currentLives--;
        UpdateHeartUI();

        if (currentLives <= 0)
        {
            gameManager.LoseLife(); // Llamar al método LoseLife del GameManager
        }
    }

    void UpdateHeartUI()
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            if (i < currentLives)
            {
                heartImages[i].sprite = fullHeartSprite; // Asignar el sprite de corazón lleno
            }
            else
            {
                heartImages[i].sprite = emptyHeartSprite; // Asignar el sprite de corazón vacío
            }
        }
    }

    void OnSceneChanged(Scene currentScene, Scene nextScene)
    {
        FindHeartImages();
        UpdateHeartUI();
    }
}