using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
This class is responsible for holding and providing all stats related to the monster
*/
[Serializable]
public abstract class ProtoBattler
{
    [field: SerializeField]
    public BattlerTemplate BattlerTemplate { get; set; }
    [field: SerializeField]
    public int Level { get; set; }
    [field: SerializeField]
    public GearSet GearActual {get; set; }

}