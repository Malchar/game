using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class Item : ScriptableObject
{
    [field: SerializeField]
    public Stats Stats {get; set; }
    [field: SerializeField]
    public string Description {get; set; }
    [field: SerializeField]
    public Sprite Image {get; set; }
}
