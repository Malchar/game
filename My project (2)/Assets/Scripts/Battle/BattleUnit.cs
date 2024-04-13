using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PartyMember partyMember;
    [SerializeField] bool isPlayerUnit;

    public PartyMember PartyMember { get => partyMember; set => partyMember = value; }

    public void Setup(){
    if (isPlayerUnit) {
        GetComponent<Image>().sprite = PartyMember.JobBase.BackSprite;
    }
    else {
        GetComponent<Image>().sprite = PartyMember.JobBase.FrontSprite;
    }
}

}
