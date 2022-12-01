using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject m_target;
    public Vector3 m_targetOffset;
    private float m_distanceCurrent;
    private float m_azimuth;
    private float m_elevation;

    public float m_panSpeed = 180.0f;   // degrees per second
    public float m_tiltSpeed = 180.0f;  // degrees per second
    public float m_tiltMin = 0.0f;
    public float m_tiltMax = 60.0f;

    public Joystick m_joystick;

    public CamInput m_camInput;
    public class CamInput
    {
        public float m_azimuthInput = 0.0f;
        public float m_elevationInput = 0.0f;
    }

    void Start()
    {
        transform.position = m_target.transform.position + new Vector3(0,3,8);
        m_camInput = new CamInput();
        m_targetOffset = new Vector3(0.0f, 0.0f, 0.0f);
        Vector3 targetPos = m_target.transform.position + m_targetOffset;
        m_distanceCurrent = Vector3.Distance(targetPos, transform.position);
        Vector3 pointP = transform.position - targetPos;
        m_azimuth = Mathf.Atan2(pointP.x, pointP.z);
        m_elevation = Mathf.Atan2(pointP.y, Mathf.Sqrt((float)(Math.Pow(pointP.x, 2) + Math.Pow(pointP.z, 2))));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || m_joystick.Horizontal < -0.1f)
        {
            m_camInput.m_azimuthInput = -1f;
            m_camInput.m_elevationInput = 0.0f;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || m_joystick.Horizontal > 0.1f)
        {
            m_camInput.m_azimuthInput = 1f;
            m_camInput.m_elevationInput = 0.0f;
        }
        else
        {
            m_camInput.m_elevationInput = 0.0f;
            m_camInput.m_azimuthInput = 0.0f;
        }
    }

    void LateUpdate()
    {
        m_azimuth += m_camInput.m_azimuthInput * m_panSpeed * Time.deltaTime * Mathf.Deg2Rad;
        if (m_azimuth > 180.0f * Mathf.Deg2Rad)
        {
            m_azimuth -= 360.0f * Mathf.Deg2Rad;
        }
        else if (m_azimuth < -180.0f * Mathf.Deg2Rad)
        {
            m_azimuth += 360.0f * Mathf.Deg2Rad;
        }
        m_elevation += m_camInput.m_elevationInput * m_tiltSpeed * Mathf.Deg2Rad * Time.deltaTime;
        m_elevation = Math.Clamp(m_elevation, 0.0f * Mathf.Deg2Rad, 60.0f * Mathf.Deg2Rad);
        Vector3 targetPos = m_target.transform.position + m_targetOffset;
        float pX = m_distanceCurrent * Mathf.Cos(m_elevation) * Mathf.Sin(m_azimuth);
        float pY = m_distanceCurrent * Mathf.Sin(m_elevation);
        float pZ = m_distanceCurrent * Mathf.Cos(m_elevation) * Mathf.Cos(m_azimuth);
        Vector3 newCameraPos = targetPos + new Vector3(pX, pY, pZ);
        //Debug.Log(newCameraPos);
        Camera.main.transform.position = newCameraPos;
        transform.LookAt(targetPos);

    }
}
