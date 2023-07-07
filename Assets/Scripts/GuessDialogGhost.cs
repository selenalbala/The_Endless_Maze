using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GuessDialogGhost : MonoBehaviour
{
    public GameObject Trophy;
    public Sprite TrophySprite;
    public GameObject dialogBox;
    public Text dialogText;
    public GameObject YNPanel;
    public Text[] optionTexts;
    public string[] dialogs;
    public string[] options;
    public string correctAnswer;
    public GameObject teleports;

    private GameManager gameManager;
    private PlayerMovement playerMovement;
    private int currentDialogIndex;
    private int selectedOption;
    private bool canInput;
    private bool playerInRange;
    private bool isDialogActive;

    private void Start()
    {
        gameManager = GameManager.instance;
        playerMovement = FindObjectOfType<PlayerMovement>();
        currentDialogIndex = 0;
        selectedOption = 0;
        canInput = false;
        playerInRange = false;
        isDialogActive = false;
        dialogBox.SetActive(false);
        YNPanel.SetActive(false);
        playerMovement.enabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInRange)
        {
            if (isDialogActive)
            {
                RestartDialog();
            }
            else if (dialogBox.activeInHierarchy)
            {
                if (currentDialogIndex < dialogs.Length - 1)
                {
                    currentDialogIndex++;
                    dialogText.text = dialogs[currentDialogIndex];
                }
                else
                {
                    dialogBox.SetActive(false);
                    YNPanel.SetActive(true);
                    canInput = true;
                }
            }
            else
            {
                StartDialog();
            }
        }

        if (canInput)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                selectedOption--;
                if (selectedOption < 0)
                    selectedOption = optionTexts.Length - 1;
                UpdateOptionTexts();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                selectedOption++;
                if (selectedOption >= optionTexts.Length)
                    selectedOption = 0;
                UpdateOptionTexts();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                ProcessAnswer(selectedOption);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            dialogBox.SetActive(false);
            YNPanel.SetActive(false);
            if (!canInput)
            {
                playerMovement.enabled = true;
            }
        }
    }

    private void StartDialog()
    {
        playerMovement.enabled = false;
        dialogBox.SetActive(true);
        dialogText.text = dialogs[currentDialogIndex];
        isDialogActive = true;
    }

    private void RestartDialog()
    {
        currentDialogIndex = 0;
        selectedOption = 0;
        canInput = false;
        dialogText.text = dialogs[currentDialogIndex];
        YNPanel.SetActive(false);
        isDialogActive = false;
    }

    private void UpdateOptionTexts()
    {
        for (int i = 0; i < optionTexts.Length; i++)
        {
            optionTexts[i].text = options[i];
            optionTexts[i].color = (i == selectedOption) ? Color.red : Color.black;
        }
    }

    private void ProcessAnswer(int selectedOption)
    {
        if (options[selectedOption] == correctAnswer)
        {
            Trophy.gameObject.GetComponent<SpriteRenderer>().sprite = TrophySprite;
            playerMovement.enabled = true;
            StartCoroutine(DisableGameObject());

            gameManager.CollectGem();
        }
        else if (options[selectedOption] == "No quiero responder")
        {
            RestartDialog();
            dialogBox.SetActive(false);
            playerMovement.enabled = true;
        }
        else
        {
            PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.LoseLife();
                dialogBox.SetActive(true);
                dialogText.text = "Has perdido una vida.";

                gameManager.LoseLife();
            }
            RestartDialog();
            dialogBox.SetActive(false);
            playerMovement.enabled = true;
        }
    }

    private IEnumerator DisableGameObject()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        teleports.SetActive(true);
        ChangeImageTrophy changeImageTrophy = Trophy.GetComponent<ChangeImageTrophy>();
        changeImageTrophy.ChangeImage(Trophy);
    }
}