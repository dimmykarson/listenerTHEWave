using UnityEngine ;
using System.Collections;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
    [SerializeField]
    protected Text timeText;
    private float defaultTime = 180;
    private float finishTime = 0;
    private bool contar = true;

    public delegate void DesafioFinish();

    public event DesafioFinish OnDesafioFinish;
    private Color orange = new Color(0.2F, 0.3F, 0.4F);

   

    void Start()
    {
        finishTime = defaultTime;
    }
    public void Restart()
    {
        finishTime = defaultTime;
        contar = true;
    }

    protected void DispatchOverEvent()
    {
        if (OnDesafioFinish != null)
            OnDesafioFinish();
    }

    void Update()
    {
        if (!contar)
        {
            return;
        }
        finishTime -= Time.deltaTime;
        if(finishTime < 0)
        {
            timeText.text = "00:00" ;
            DispatchOverEvent();
        }
        else
        {
            int min = (int)finishTime / 60;
            string minutos = (min).ToString();
            int sec = (int)finishTime % 60;
            string segundos = "00";
            if (sec == 60) sec = 0;
            if (sec < 10)
            {
                segundos = "0"+(sec).ToString("f0");
            }
            else
            {
                segundos = (sec).ToString("f0");
            }
            

            timeText.text = minutos + ":" + segundos;
            if (min < 1)
            {
                timeText.color = Color.red;
            }else if (min < 2)
            {
                timeText.color = Color.yellow;
            }
            else
            {
                timeText.color = Color.green;
            }
            
        }

    }
    public bool Contar
    {
        get
        {
            return contar;
        }

        set
        {
            contar = value;
        }
    }
}
