using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class LimitTime : MonoBehaviour
{
    [SerializeField, Header("制限時間テキストuI")]
    private TMP_Text _timeText;

    [SerializeField]
    private Compas compas;

    private const float LIMITTIME = 180f;

    private float _time = 0;

    private FadeScene fadeOut;
    private bool isGame = true;

    private void Awake()
    {
        _time = LIMITTIME;
        fadeOut = GetComponent<FadeScene>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (RandomUIPrefabSelector.success01 && RandomUIPrefabSelector.success02 && RandomUIPrefabSelector.success03 && isGame) 
        {
            Debug.Log("GameClear!!");
            isGame = false;
            compas.enabled = false;
            fadeOut.FadeOutandLoadScene("GameClear"); 
        }
        if (isGame)
        {
            _time -= Time.deltaTime;    
            _time = Mathf.Clamp(_time, 0, LIMITTIME);
            DisplayTime((int)_time);
            if (_time <= 0)
            {
                isGame = false;
                compas.enabled = false;
                fadeOut.FadeOutandLoadScene("GameOver");
            }
        }
    }

    void DisplayTime(int time)
    {
        int second = time;
        int minute = 0;

        while (second >= 60)
        {
            second -= 60;
            minute++;
        }

        _timeText.text = minute.ToString("00") + " : " + second.ToString("00");
    }
}
