using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
This class is responsible for holding and providing all stats related to the monster
*/
[Serializable]
public class ProtoBattler
{
    [field: SerializeField]
    public BattlerTemplate BattlerTemplate { get; set; }
    [field: SerializeField]
    public int Level { get; set; }
    [field: SerializeField]
    public string Name { get; set; }
    [SerializeField]
    public GearSet GearActual {get; set; }

    public GearSet GetGear() {
        if (GearActual == null) return BattlerTemplate.GearCore;
        else return GearActual;
    }

    public ElementVector GetTotalResistances() {
        ElementVector resistances = BattlerTemplate.ResistancesCore;
        foreach(CreatureType c in BattlerTemplate.CreatureTypes)
        resistances.Append(c.Resistances);
        return resistances;
    }
}