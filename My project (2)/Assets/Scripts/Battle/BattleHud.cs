using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] HPBar hpBar;

    public void SetData(PartyMember partyMember){
        nameText.SetText(partyMember.Name);
        levelText.SetText("Lvl " + partyMember.Level);
        hpBar.SetHP((float) partyMember.HP / partyMember.GetMaxHP());
    }
}
