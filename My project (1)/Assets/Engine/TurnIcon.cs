using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnIcon : MonoBehaviour
{
    public Image IconImage;

    public void Setup(Sprite icon)
    {
        IconImage.sprite = icon;
    }
}
