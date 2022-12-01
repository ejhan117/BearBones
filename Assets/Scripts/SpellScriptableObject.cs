using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell", menuName ="Spell")]
public class SpellScriptableObject : ScriptableObject
{
    public float m_damage = 5.0f;
    public float m_lifetime = 2.0f;
    public float m_speed = 15.0f;
    public float m_spellRadius = 0.5f;

}
