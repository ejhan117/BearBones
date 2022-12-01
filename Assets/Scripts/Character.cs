using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Character;

public class Character : MonoBehaviour
{
    public CharInput m_charInput;
    CharacterController m_characterController;
    public float m_walkSpeed = 4.0f;
    public float m_turnSpeed = 360.0f;
    public Rigidbody rb;
    private bool m_isCasting = false;
    private float m_lastCastTime = 0.0f;
    private float m_castCooldown = 0.5f;
    public Spell m_spell;
    public Transform m_spellCastPoint;
    private bool m_isFrozen = false;
    private bool m_doubleShot = false;
    public GameObject m_canvas;
    public int m_maxHealth = 5;
    public int m_currHealth = 5;
    public float m_damageTimer = 0.0f;
    public bool m_damaged = false;
    public GameObject m_gameOver;
    public Collider m_collider;
    public Joystick m_joystick;
    public Animator m_anim;
    public class CharInput
    {
        public Vector3 m_movement;
        public float m_angle;
        public bool m_attacking = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        m_charInput = new CharInput();
        m_characterController = GetComponent<CharacterController>();
        m_doubleShot = false;
        m_gameOver.SetActive(false);
        m_collider = GetComponent<Collider>();
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(!m_characterController.isGrounded) {
        //    m_charInput.m_movement = m_charInput.m_movement + Physics.gravity;
        //}
        if (m_isFrozen)
        {
            return;
        }
        m_lastCastTime += Time.deltaTime;
        Vector3 moveTemp = new Vector3(0, 0, 0);
        Vector3 temp = Camera.main.transform.forward;
        Vector3 forward = transform.forward;
        float forwardFloat = (float)Math.Atan2(forward.z, forward.x);
        float forwardFloatFull = forwardFloat;
        m_charInput.m_angle = (float)Math.Atan2(temp.z, temp.x);
        float m_angleFull = m_charInput.m_angle;
        if (forwardFloat < 0.0f)
        {
            forwardFloatFull = forwardFloat + (float)Math.PI * 2;
        }
        if (m_angleFull < 0.0f)
        {
            m_angleFull = m_angleFull + (float)Math.PI * 2;
        }
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.W) || m_joystick.Vertical > 0.3f)
        {
            m_charInput.m_movement += Camera.main.transform.forward.normalized;
            m_anim.SetBool("Run Forward", true);
        }
        if (Input.GetKey(KeyCode.S) || m_joystick.Vertical < -0.3f)
        {
            m_charInput.m_movement += -Camera.main.transform.forward.normalized;
            m_anim.SetBool("Run Forward", true);
        }
        if (Input.GetKey(KeyCode.A) || m_joystick.Horizontal < -0.3f)
        {
            m_charInput.m_movement += -Camera.main.transform.right.normalized;
            m_anim.SetBool("Run Forward", true);
        }
        if (Input.GetKey(KeyCode.D) || m_joystick.Horizontal > 0.3f)
        {
            m_charInput.m_movement += Camera.main.transform.right.normalized;
            m_anim.SetBool("Run Forward", true);
        }
        if(m_charInput.m_movement == Vector3.zero)
        {
            m_anim.SetBool("Run Forward", false);
        }
        m_charInput.m_movement -= new Vector3(0,m_charInput.m_movement.y,0);
        m_characterController.SimpleMove(m_charInput.m_movement * m_walkSpeed);
        if(m_charInput.m_movement != Vector3.zero)
        {
            transform.LookAt(transform.position + m_charInput.m_movement);
            //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_charInput.m_movement), 0.005F);
        }
        m_charInput.m_movement = Vector3.Normalize(m_charInput.m_movement);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CastSpell();
        }
        if (m_isCasting)
        {
            m_lastCastTime += Time.deltaTime;
            if(m_lastCastTime > m_castCooldown)
            {
                m_isCasting = false;
            }
        }
        if (m_damaged)
        {
            m_damageTimer += Time.deltaTime;
            if(m_damageTimer > 2.0f)
            {
                m_damageTimer = 0.0f;
                m_damaged = false;
            }
        }
        m_charInput.m_movement = Vector3.zero;
    }

    public void CastSpell()
    {
        if (!m_isCasting)
        {
            m_isCasting = true;
            m_lastCastTime = 0.0f;
            Instantiate(m_spell, m_spellCastPoint.position, m_spellCastPoint.rotation);
        }

    }

    public void StopMoving()
    {
        m_isFrozen = true;
    }

    public void StartMoving()
    {
        m_isFrozen = false;
    }

    public void IncreaseHealth()
    {
        m_maxHealth++;
        m_currHealth = m_maxHealth;
        m_canvas.SetActive(false);
        Time.timeScale = 1.0f;
        StartMoving();
    }

    public void IncreaseWalkSpeed(float amount)
    {
        m_walkSpeed += amount;
        m_canvas.SetActive(false);
        Time.timeScale = 1.0f;
        StartMoving();
    }

    public void CastFaster(float amount)
    {
        m_castCooldown -= amount;
        m_canvas.SetActive(false);
        Time.timeScale = 1.0f;
        StartMoving();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "SkeletonWarrior(Clone)" && !m_damaged)
        {
            TakeDamage();
        }
        else if(other.gameObject.name  == "Quad")
        {
            Physics.IgnoreCollision(m_collider, other);
        }
    }

    private void TakeDamage()
    {
        m_currHealth--;
        m_damaged = true;
        if(m_currHealth == 0)
        {
            m_anim.SetBool("Death", true);
            GameOver();
        }
    }

    public void GameOver()
    {
        m_gameOver.SetActive(true);
        StopMoving();
        Time.timeScale = 0.0f;
    }

    public int GetHealth()
    {
        return m_currHealth;
    }
}
