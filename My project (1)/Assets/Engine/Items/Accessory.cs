using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(fileName = "new Accessory", menuName = "ScriptableObjects/Items/Accessory", order = 3)]
public class Accessory : Equipment
{
    [field: SerializeField]
    public string ExtraDescription {get; set; }

}
