using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float m_moveSpeed;
    public GameObject m_character;
    public Collider m_collider;
    // Start is called before the first frame update
    void Start()
    {
        m_moveSpeed = 2.0f;
        m_character = GameObject.Find("Player");
        m_collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(m_character.transform.position);
        transform.position += transform.forward * m_moveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Quad")
        {
            Physics.IgnoreCollision(m_collider, other);
        }
        else if(other.gameObject.name == "Player")
        {
            transform.position -= transform.forward;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
