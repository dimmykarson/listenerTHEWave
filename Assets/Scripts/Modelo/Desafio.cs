using UnityEngine;
using System.Collections;
using System;

public class Desafio : MonoBehaviour {
    private Dificuldade dificuldade;
    private Melodia[] melodias;

    

    public delegate void CheckDelegate();
    public event CheckDelegate OnChecker;

    void Awake()
    {
        melodias = GetComponentsInChildren<Melodia>();
    }

    //Checa se as notas em memória são as mesmas da melodia
    //Retorna 0 se não tem as condições necessárias
    //Retorna 1 se as notas batem com a melodia
    //Retorna -1 se não batem na sequência
    public int Checar(string[] notas)
    {
        if (notas == null) return 0;
        if (notas.Length == 0) return 0;
        if (notas.Length != melodias.Length) return 0;
        for(int i = 0; i < melodias.Length; i++)
        {
            if (melodias[i].Comparar(notas[i]))
            {
                DispacherOnChecker();
                return -1;
            }
        }
        return 1;
    }

    private void DispacherOnChecker()
    {
        if (OnChecker != null)
        {
            OnChecker();
        }
    }

    public Melodia[] Melodias
    {
        get
        {
            return melodias;
        }

        set
        {
            melodias = value;
        }
    }

    public Dificuldade Dificuldade
    {
        get
        {
            return dificuldade;
        }

        set
        {
            dificuldade = value;
        }
    }
}
