using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Serializable]
public class GearSet
{
    [field: SerializeField]
    public Armament MainHand {get; set; }
    [field: SerializeField]
    public Armament OffHand {get; set; }
    [field: SerializeField]
    public BodyArmor BodyArmor {get; set; }
    [field: SerializeField]
    public Accessory AccessoryA {get; set; }
    [field: SerializeField]
    public Accessory AccessoryB {get; set; }

    // TODO: methods for equipping/removing items, and validating hand types
}
