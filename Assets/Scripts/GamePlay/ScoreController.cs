using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class ScoreController : MonoBehaviour {
    [SerializeField]
    protected Text text;
    public delegate void ScoreDelegate(int prevValue, int currentValue);

    public event ScoreDelegate OnScoreChange;
    public event ScoreDelegate OnScoreReset;

    private int previousValue;
    private int currentValue;

    void Start()
    {
        ChangeValue(0);
    }

    public void Resetar()
    {
        currentValue = 0;
    }

    public void ChangeValue(int increment)
    {
        if (increment < 0) return;
        currentValue += increment;
        text.text = "Score: " + currentValue + " pontos ";
        if (OnScoreChange != null)
        {
            OnScoreChange(0, increment);
        }
    }

    protected void DispatchResetEvent(int prevValue, int currentValue)
    {
        if (OnScoreReset != null)
            OnScoreReset(prevValue, currentValue);
    }

    protected int CurrentValue
    {
        get
        {
            return currentValue;
        }

        set
        {
            currentValue = value;
        }
    }

    protected int PreviousValue
    {
        get
        {
            return previousValue;
        }

        set
        {
            previousValue = value;
        }
    }
}
