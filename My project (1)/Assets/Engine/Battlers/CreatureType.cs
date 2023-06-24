using UnityEngine;

[CreateAssetMenu(fileName = "new Creature Type", menuName = "ScriptableObjects/CreatureType", order = 2)]
public class CreatureType : ScriptableObject
{
    [field: SerializeField]
    public ElementVector Resistances {get; set; }
}
