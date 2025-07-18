using System;
using TMPro;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;

public class HUDController : MonoBehaviour
{

    private Camera cam;

    public MonoBehaviour playerMovement;
    public static HUDController instance;

    String[] hiraganaArray = new String[10];

    private int currentHiraganaNum = 1;

    [SerializeField] public GameObject dictionary;

    private bool isUIOpen = false;

    private void Awake()
    {
        instance = this;
        InvokeRepeating("CountDown", 1, 1);
        hiraganaArray[0] = "あ";
        hiraganaArray[1] = "い";
        hiraganaArray[2] = "え";
        hiraganaArray[3] = "う";
        hiraganaArray[4] = "お";
        hiraganaArray[5] = "か";
        hiraganaArray[6] = "き";
        hiraganaArray[7] = "け";
        hiraganaArray[8] = "く";
        hiraganaArray[9] = "こ";

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isUIOpen = !isUIOpen;
            dictionary.SetActive(isUIOpen);

            if (isUIOpen)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                playerMovement.enabled = false;
                // Optionally disable your player movement or camera script here
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                playerMovement.enabled = true;
                // Optionally re-enable your player movement/camera
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(dictionaryText, Input.mousePosition, cam);

            if (linkIndex != -1)
            {
                TMP_LinkInfo linkInfo = dictionaryText.textInfo.linkInfo[linkIndex];
                string linkID = linkInfo.GetLinkID();

                Debug.Log($"You clicked: {linkInfo.GetLinkText()}");

                if (linkInfo.GetLinkText().ToString().Contains(hiraganaToFind.text))
                {
                    SoundController.instance.correctChosen();
                    ChangeHiragana();
                }
            }
        }
    }

    [SerializeField] TMP_Text onHoverText;
    [SerializeField] TMP_Text dictionaryText;
    [SerializeField] TMP_Text timerText;

    [SerializeField] TMP_Text hiraganaToFind;

    private int time = 60;

    public void EnableInteractionText(string text)
    {
        onHoverText.text = text + " (E)";
        onHoverText.gameObject.SetActive(true);
    }

    public void DisableInteractionText()
    {
        onHoverText.gameObject.SetActive(false);
    }

    internal void AddToDictionary(string message)
    {
        if (dictionaryText.text.Contains(message))
        {
            //WORD ALREADY IN DICTIONARY
        }
        else
        {
            dictionaryText.text = dictionaryText.text + "\n" + message;
        }

    }

    void CountDown()
    {
        time = time - 1;
        timerText.text = time.ToString();
        if (time == 0)
        {
            GameStates.instance.gameOver();
        }
    }

    public void ChangeHiragana()
    {
        Debug.Log("CURRENT HIRAGANA NUM: "+currentHiraganaNum);
        if (currentHiraganaNum == 10)
        {
            GameStates.instance.youWin();
            GameStates.instance.gameEnded = true;
            SoundController.instance.Victory();
        }
        else
        {
            //set correct hiragana to color
            string originalText = dictionaryText.text;
            string targetHiragana = hiraganaToFind.text;
            string coloredHiragana = "<color=red>"+hiraganaToFind.text+"</color>";

            string updatedText = originalText.Replace(targetHiragana, coloredHiragana);

            dictionaryText.text = updatedText;
            hiraganaToFind.text = hiraganaArray[currentHiraganaNum];
            currentHiraganaNum++;
        }


    }
}
