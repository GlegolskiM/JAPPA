using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    Outline outline;
    public string message;
    public UnityEvent onInteraction;

    bool hasLogged = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        outline = GetComponent<Outline>();
        DisableOutline();
    }

    public void Interact()
    {
        if (!hasLogged)
        {
            HUDController.instance.AddToDictionary("<link='1'>" + message  +"</link>");
            SoundController.instance.AddToDictionary();
            onInteraction.Invoke();
        }
        hasLogged = true;
        Invoke(nameof(ResetLogFlag), 0.2f); // Reset after 0.2 seconds

    }

        void ResetLogFlag()
    {
        hasLogged = false;
    }

    public void DisableOutline()
    {
        outline.enabled = false;
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
}
