using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public static class CombatTools
{
    public static ElementVector CalculateDamageOut(Battler user, PoweredElement damage) {
        // get relevant attribute
        int attribute = 0;
        switch(damage.PowerSource){
            case PowerSource.Physical: attribute = user.GetStats().Brawn;
            break;
            case PowerSource.Dexterity: attribute = user.GetStats().Agility;
            break;
            case PowerSource.Magical: attribute = user.GetStats().Courage;
            break;
            default:
            break;
        }
        return ElementVector.Scale(attribute, damage.ElementVector);
    }

    public static int CalculateDamageIn(Battler target, ElementVector damage) {
        int finalDamage = 0;
        int agility = target.GetStats().Agility;
        int brawn = target.GetStats().Brawn;
        int courage = target.GetStats().Courage;

        // physical
        finalDamage += ReduceDamage(brawn + target.GetArmor().Blunt, damage.Blunt);
        finalDamage += ReduceDamage(brawn + target.GetArmor().Pierce, damage.Pierce);
        finalDamage += ReduceDamage(brawn + target.GetArmor().Slash, damage.Slash);
        finalDamage += ReduceDamage(agility + target.GetArmor().Tech, damage.Tech);

        // elemental
        finalDamage += ReduceDamage(courage + target.GetResistances().Earth, damage.Earth);
        finalDamage += ReduceDamage(courage + target.GetResistances().Fire, damage.Fire);
        finalDamage += ReduceDamage(courage + target.GetResistances().Wind, damage.Wind);
        finalDamage += ReduceDamage(courage + target.GetResistances().Water, damage.Water);
        finalDamage += ReduceDamage(courage + target.GetResistances().Spirit, damage.Spirit);

        // other
        finalDamage += ReduceDamage(courage + target.GetResistances().Bio, damage.Bio);
        finalDamage += ReduceDamage(courage + target.GetResistances().Electric, damage.Electric);
        finalDamage += ReduceDamage(courage + target.GetResistances().Psychic, damage.Psychic);
        finalDamage += ReduceDamage(courage + target.GetResistances().Ice, damage.Ice);
        finalDamage += ReduceDamage(courage + target.GetResistances().Dark, damage.Dark);
        
        return finalDamage;
    }

    private static int ReduceDamage(int modifier, int damage){
        int finalDamage = damage - modifier;
        if (finalDamage <= 0) return 0;
        else return finalDamage;
    }

    public static void DealDamage(Battler user, PoweredElement damage, Battler target) {
        // TODO: intervene here for dodge/miss. Can use a return type indicator.
        target.TakeDamage(CalculateDamageIn(target, CalculateDamageOut(user, damage)));
    }

    public static int BoundValue(int min, int value, int max) {
        if (min > max) throw new Exception("trying to bound value but min > max");
        if (value < min) value = min;
        else if (value > max) value = max;
        return value;
    }
}
