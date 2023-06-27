using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Equipment : Item
{
    [field: SerializeField]
    public ElementVector ArmorValue {get; set; }

}
