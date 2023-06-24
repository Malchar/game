using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[Serializable]
public class ElementVector
{
    [field: SerializeField]
    public int Earth { get; set; }
    [field: SerializeField]
    public int Fire { get; set; }
    [field: SerializeField]
    public int Wind { get; set; }
    [field: SerializeField]
    public int Water { get; set; }

    public ElementVector(int earth, int fire, int wind, int water)
    {
        this.Earth = earth;
        this.Fire = fire;
        this.Wind = wind;
        this.Water = water;

    }

    public void Append(ElementVector v) {
        this.Earth += v.Earth;
        this.Fire += v.Fire;
        this.Wind += v.Wind;
        this.Water += v.Water;
    }

    public static ElementVector Add(ElementVector v1, ElementVector v2) {
        int earth = v1.Earth + v2.Earth;
        int fire = v1.Fire + v2.Fire;
        int wind = v1.Wind + v2.Wind;
        int water = v1.Water + v2.Water;
        return new ElementVector(earth, fire, wind, water);
    }

}