using UnityEngine ;
using System.Collections;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
    [SerializeField]
    protected Text timeText;
    private float startTime;
    private float finishTime = 180;
    public delegate void TimerDelegate();
    public event TimerDelegate OnFinisher;
    void Start()
    {
    }
    protected void DispatchOverEvent()
    {
        if (OnFinisher != null)
            OnFinisher();
    }

    void Update()
    {
        finishTime -= Time.deltaTime;
        if(finishTime < 0)
        {

            timeText.text = "00:00" ;
            DispatchOverEvent();
        }
        else
        {

            string minutos = ((int)finishTime / 60).ToString();
            string segundos = (finishTime % 60).ToString("f0");

            timeText.text = minutos + ":" + segundos;
            
        }

    }
}
