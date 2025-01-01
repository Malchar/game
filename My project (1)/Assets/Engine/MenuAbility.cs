using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuAbility : MonoBehaviour
{
    public TMP_Text abilityText; // Reference to the text component
    public Image background;
    private Color normalColor = Color.white; // Default color
    private Color highlightColor = Color.yellow; // Highlight color

    public void SetText(string abilityName)
    {
        abilityText.text = abilityName;
    }

    public void SetHighlighted(bool isHighlighted)
    {
        if (background != null)
        {
            background.color = isHighlighted ? highlightColor : normalColor;
        }
    }
}
