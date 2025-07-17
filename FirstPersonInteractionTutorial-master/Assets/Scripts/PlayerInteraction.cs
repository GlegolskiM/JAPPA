using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f; //how far player has to be from object
    Interactable currentInteractable; //what object is being interacted with

    private TextMeshProUGUI dictionaryText;
    private TextMeshProUGUI targetHiragana;


    bool hasLogged = false;

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyUp(KeyCode.E) && currentInteractable != null && !hasLogged)
        {
            currentInteractable.Interact();
            HUDController.instance.DisableInteractionText();


            dictionaryText = GameObject.Find("Dictionary").GetComponent<TextMeshProUGUI>();
            string fullText = dictionaryText.text;

            targetHiragana = GameObject.Find("Hiragana to find").GetComponent<TextMeshProUGUI>();
            string targetWord = targetHiragana.text;
            Debug.Log("This is the full text: " + fullText);
            if (fullText.Contains(targetWord))
            {
                Debug.Log($"Found '{targetWord}' in dictionary!");
                SoundController.instance.AddToDictionary();
                HUDController.instance.ChangeHiragana();
            }
            else
            {
                Debug.Log($"'{targetWord}' not found.");
            }
                hasLogged = true;
                Invoke(nameof(ResetLogFlag), 0.2f); // Reset after 0.2 seconds
        }

    }

    void ResetLogFlag()
    {
        hasLogged = false;
    }

    void CheckInteraction()
    {

        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out hit, playerReach))
        {

            if (hit.collider.tag == "Interactable")//if looking at interactable 
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (currentInteractable && newInteractable != currentInteractable)//if in consequent frames u look at two different interactables
                {
                    currentInteractable.DisableOutline();
                }

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else //if interactable is not enabled
                {
                    DisableCurrentInteractable();
                }
            }
            else //if object we are looking at is not interactable
            {
                DisableCurrentInteractable();
            }
        }
        else //if nothing is within reach
        {
            DisableCurrentInteractable();
        }
        

    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline();
        HUDController.instance.EnableInteractionText(currentInteractable.message);
    }

    void DisableCurrentInteractable()
    {
        HUDController.instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable.DisableOutline();
            currentInteractable = null;
        }


    }
}
