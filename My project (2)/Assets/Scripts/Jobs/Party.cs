using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] List<PartyMember> partyMembers;

    private void Start(){
        foreach (var member in partyMembers){
            member.Init();
        }
    }

    /**
    *   returns first party member that has HP or null if none.
    */
    public PartyMember GetHealthyMember(){
        return partyMembers.Where(x => x.HP > 0).FirstOrDefault();
    }
}
