using UnityEngine;
using System.Collections;

public class StarController : MonoBehaviour {
    public Star[] stars;
    private int chance = 3;


    public delegate void ChanceDelegate();
    public event ChanceDelegate OnFinisher;
    void Awake()
    {
        stars = GetComponentsInChildren<Star>();
        chance = 3;
    }

    public void Resetar()
    {
        chance = 3;
        for(int i = 0; i < stars.Length; i++)
        {
            stars[i].TurnOn();
        }
    }

    public void DecrementarChances()
    {
        int pos = chance - 1;
        if (pos < 0) pos = 0;
        stars[pos].TurnOff();
        chance--;

    }

    public void DispacherFinishChance()
    {
        if (chance < 0 && OnFinisher!=null)
        {
            OnFinisher();
        }
    }

    public int Chance
    {
        get
        {
            return chance;
        }

        set
        {
            chance = value;
        }
    }

}
