using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattlerUI : MonoBehaviour
{
    [SerializeField] private Image image; // TODO this is just for debug development
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text statusText;
    [SerializeField] private GameObject selectionCursor;
    // graphics
    [SerializeField] public SpriteRenderer SpriteRenderer {get; set; }
    [SerializeField] public Animator Animator {get; set; }
    

    public void ShowCursor(bool show)
    {
        selectionCursor.SetActive(show);
    }
    
    public void Bind(Battler battler)
    {
        // subscribe to events
        battler.OnHealthChanged += UpdateHealthBar;

        // initialize values
        healthSlider.maxValue = battler.HP;
        healthSlider.value = battler.HP;
        nameText.SetText(battler.GetName());
    }

    private void UpdateHealthBar(int currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    // Start is called before the first frame update
    void Start()
    {
        statusText.SetText("");
        ShowCursor(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
