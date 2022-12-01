using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public Character m_character;
    public GameObject m_gameObject;
    public float m_elapsedTime;
    public int counter;
    public float m_timeBetween;
    public float m_gameTimer;
    public TMP_Text m_timeText;
    private int m_waveNum = 0;
    public GameObject m_canvas;
    public Button m_healthButton;
    public Button m_speedButton;
    public Button m_cooldownButton;
    public Button m_restartButton;
    public TMP_Text m_healthText;
    // Start is called before the first frame update
    void Start()
    {
        m_timeBetween = 2.0f;
        m_elapsedTime = 0.0f;
        counter = 0;
        for(int i = 0; i < 20; i++)
        {
            GenerateEnemy();
        }
        m_gameTimer = 0.0f;
        m_canvas.SetActive(false);
        ButtonInit();
    }

    // Update is called once per frame
    void Update()
    {
        m_gameTimer += Time.deltaTime;
        int minutes = (int)m_gameTimer / 60;
        int seconds = (int)m_gameTimer % 60;
        int milliseconds = (int)(m_gameTimer * 10) % 10;
        if (m_timeText != null)
        {
            m_timeText.text = string.Format("{0:00}:{1:00}:{2:0}", minutes, seconds, milliseconds);
        }
        m_elapsedTime += Time.deltaTime;
        if(m_elapsedTime >= m_timeBetween)
        {
            GenerateEnemy();
            counter++;
            m_elapsedTime = 0.0f;
        }

        if(m_gameTimer >= 120.0f)
        {
            //Wave Complete
            if (m_waveNum > 5)
            {
                m_character.GameOver();
            }
            Pause();
            m_gameTimer = 0.0f;
            m_waveNum++;
            m_timeBetween -= 0.2f;
        }
        m_healthText.text = "Health: " + m_character.GetHealth();
    }

    void GenerateEnemy()
    {
        GameObject clone = Instantiate(m_gameObject);
        int negOrNot = Random.Range(0, 2);
        if (negOrNot != 1)
        {
            negOrNot = -1;
        }
        int negOrNotTwo = Random.Range(0, 2);
        if (negOrNotTwo != 1)
        {
            negOrNotTwo = -1;
        }
        clone.transform.position = m_character.transform.position + new Vector3(negOrNot * Random.Range(10.0f, 15.0f), 0, negOrNotTwo * Random.Range(10.0f,20.0f));
    }

    void Pause()
    {
        Time.timeScale = 0.0f;
        m_canvas.SetActive(true);
    }

    void ButtonInit()
    {
        //GameObject.Find("Upgrade1").GetComponentInChildren<TMP_Text>().text = "Increase Maximum Health and Heal!";
        //GameObject.Find("Upgrade2").GetComponentInChildren<TMP_Text>().text = "Increase Walking Speed!";
        //GameObject.Find("Upgrade3").GetComponentInChildren<TMP_Text>().text = "Decrease Cooldown for Attack!";
        //GameObject.Find("RestartGame").GetComponentInChildren<TMP_Text>().text = "Restart Game";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;

    }
}
