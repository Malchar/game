using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// represents the amount of damage dealt per relevant stat point
[Serializable]
public class ElementVector
{
    [field: SerializeField]
    public int Blunt { get; set; }
    [field: SerializeField]
    public int Pierce { get; set; }
    [field: SerializeField]
    public int Slash { get; set; }
    [field: SerializeField]
    public int Tech { get; set; }
    [field: SerializeField]
    public int Earth { get; set; }
    [field: SerializeField]
    public int Fire { get; set; }
    [field: SerializeField]
    public int Wind { get; set; }
    [field: SerializeField]
    public int Water { get; set; }
    [field: SerializeField]
    public int Spirit { get; set; }
    [field: SerializeField]
    public int Bio { get; set; }
    [field: SerializeField]
    public int Electric { get; set; }
    [field: SerializeField]
    public int Psychic { get; set; }
    [field: SerializeField]
    public int Ice { get; set; }
    [field: SerializeField]
    public int Dark { get; set; }

    public ElementVector() {

    }

    public ElementVector(int blunt, int pierce, int slash, int tech, int earth, int fire, int wind, int water, int spirit, int bio, int electric, int psychic, int ice, int dark) 
    {
        this.Blunt = blunt;
        this.Pierce = pierce;
        this.Slash = slash;
        this.Tech = tech;
        this.Earth = earth;
        this.Fire = fire;
        this.Wind = wind;
        this.Water = water;
        this.Spirit = spirit;
        this.Bio = bio;
        this.Electric = electric;
        this.Psychic = psychic;
        this.Ice = ice;
        this.Dark = dark;
   
    }

    public static ElementVector Scale(int k, ElementVector v) {
        int blunt = k * v.Blunt;
        int pierce = k * v.Pierce;
        int slash = k * v.Slash;
        int tech = k * v.Tech;
        int earth = k * v.Earth;
        int fire = k * v.Fire;
        int wind = k * v.Wind;
        int water = k * v.Water;
        int spirit = k * v.Spirit;
        int bio = k * v.Bio;
        int electric = k * v.Electric;
        int psychic = k * v.Psychic;
        int ice = k * v.Ice;
        int dark = k * v.Dark;
        return new ElementVector(blunt, pierce, slash, tech, earth, fire, wind, water, spirit, bio, electric, psychic, ice, dark);
    }

        public void Append(ElementVector v) {
        this.Blunt += v.Blunt;
        this.Pierce += v.Pierce;
        this.Slash += v.Slash;
        this.Tech += v.Tech;
        this.Earth += v.Earth;
        this.Fire += v.Fire;
        this.Wind += v.Wind;
        this.Water += v.Water;
        this.Spirit += v.Spirit;
        this.Bio += v.Bio;
        this.Electric += v.Electric;
        this.Psychic += v.Psychic;
        this.Ice += v.Ice;
        this.Dark += v.Dark;
    }

}