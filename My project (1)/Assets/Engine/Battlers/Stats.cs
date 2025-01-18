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

    public static Stats Add(params Stats[] statsArray)
    {
        int totalAgility = 0;
        int totalBrawn = 0;
        int totalCourage = 0;
        foreach (Stats stats in statsArray) {
            totalAgility += stats.Agility;
            totalBrawn += stats.Brawn;
            totalCourage += stats.Courage;
        }
        return new Stats(totalAgility, totalBrawn, totalCourage);
    }

    public Stats Floor() {
        if (this.Agility < 0) {
            this.Agility = 0;
        }
        if (this.Brawn < 0) {
            this.Brawn = 0;
        }
        if (this.Courage < 0) {
            this.Courage = 0;
        }
        return this;
    }

    public static Stats Scale(int scaler, Stats stats) {
        return new Stats(scaler * stats.Agility, scaler * stats.Brawn, scaler * stats.Courage);
    }

    private static int LevellerFunction(int core, int level, int growth){
        return core + level * growth;
    }

    public static Stats LevelledStats(Stats core, int level, Stats growth) {
        return new Stats(
            LevellerFunction(core.Agility, level, growth.Agility),
            LevellerFunction(core.Brawn, level, growth.Brawn),
            LevellerFunction(core.Courage, level, growth.Courage)
        );
    }

}