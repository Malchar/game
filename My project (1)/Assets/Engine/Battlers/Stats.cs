using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;

[Serializable]
public class Stats
{
    [field: SerializeField]
    public int Agility { get; set; }
    [field: SerializeField]
    public int Brawn { get; set; }
    [field: SerializeField]
    public int Courage { get; set; }

    public Stats(int agility, int brawn, int courage)
    {
        this.Agility = agility;
        this.Brawn = brawn;
        this.Courage = courage;
    }

    public static Stats Add(Stats statsX, Stats statsY)
    {
        return new Stats(statsX.Agility + statsY.Agility, statsX.Brawn + statsY.Brawn, statsX.Courage + statsY.Courage);
    }

    public static Stats Scale(int scaler, Stats stats) {
        return new Stats(scaler * stats.Agility, scaler * stats.Brawn, scaler * stats.Courage);
    }


}