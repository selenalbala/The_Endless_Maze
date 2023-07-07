using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImageTrophy : MonoBehaviour
{
    public Sprite newSprite;
    private Image imageComponent;

    private void Start()
    {
        imageComponent = GetComponent<Image>();
    }

    public void ChangeImage(GameObject trophy)
    {
        Image trophyImage = trophy.GetComponent<Image>();
        if (trophyImage != null && newSprite != null)
        {
            trophyImage.sprite = newSprite;
        }
    }
}