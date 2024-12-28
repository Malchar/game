using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattlerUI : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text statusText;
    public Battler Battler { get; private set; }
    
    public void Setup(Battler battler) {
        Battler = battler;
        nameText.SetText(battler.GetName());
        healthSlider.SetValueWithoutNotify(100);
        statusText.SetText("");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
