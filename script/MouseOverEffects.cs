using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverEffects : MonoBehaviour, IPointerEnterHandler
{
    public AudioSource mouseOverSound;
    public Color mouseOverColor = Color.yellow; 
    private Color originalColor;
    private Text buttonText;

    void Start()
    {
        buttonText = GetComponentInChildren<Text>();
        if (buttonText != null)
        {
            originalColor = buttonText.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("On pointer");
        if (mouseOverSound != null)
        {
            mouseOverSound.Play();
        }

        if (buttonText != null)
        {
            buttonText.color = mouseOverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (buttonText != null)
        {
            buttonText.color = originalColor;
        }
    }
}
