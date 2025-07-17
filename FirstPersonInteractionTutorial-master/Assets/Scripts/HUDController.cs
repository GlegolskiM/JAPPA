using System;
using TMPro;
using Unity.Multiplayer.Center.Common.Analytics;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public static HUDController instance;

    String[] hiraganaArray = new String[5];

    private int currentHiraganaNum = 1;


    private void Awake()
    {
        instance = this;
        InvokeRepeating("CountDown", 1, 1);
        hiraganaArray[0] = "あ";
        hiraganaArray[1] = "い";
        hiraganaArray[2] = "え";
        hiraganaArray[3] = "う";
        hiraganaArray[4] = "お";

    }

    [SerializeField] TMP_Text onHoverText;
    [SerializeField] TMP_Text dictionaryText;
    [SerializeField] TMP_Text timerText;

    [SerializeField] TMP_Text hiraganaToFind;

    private int time = 20;

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
        dictionaryText.text = dictionaryText.text + "\n" + message;
    }

    void CountDown()
    {
        timerText.text = time.ToString();
    }

    public void ChangeHiragana()
    {
        hiraganaToFind.text = hiraganaArray[currentHiraganaNum];
        currentHiraganaNum++;
    }
}
