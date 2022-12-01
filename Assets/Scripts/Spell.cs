using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public SpellScriptableObject SpellToCast;

    public GameObject m_floor;
    public GameObject m_character;
    public SphereCollider m_collider;
    public Rigidbody m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_collider = GetComponent<SphereCollider>();
        m_collider.isTrigger = true;
        m_collider.radius = SpellToCast.m_spellRadius;

        m_rb = GetComponent<Rigidbody>();
        m_rb.isKinematic = true;

        Physics.IgnoreCollision(m_collider, m_floor.GetComponent<Collider>());
        Physics.IgnoreCollision(m_collider, m_character.GetComponent<Collider>());



        Destroy(this.gameObject, SpellToCast.m_lifetime);

    }

    // Update is called once per frame
    void Update()
    {
        if(SpellToCast.m_speed > 0)
        {
            transform.Translate(Vector3.forward * SpellToCast.m_speed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Quad" || other.gameObject.name == "Player" || other.gameObject.name == "FireballSpell(Clone)")
        {
            Physics.IgnoreCollision(m_collider, other);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
