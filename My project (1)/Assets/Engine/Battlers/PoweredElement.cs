using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class PoweredElement
{
    [field: SerializeField]
    public PowerSource PowerSource {get; set; }
    [field: SerializeField]
    public ElementVector ElementVector {get; set; }
}