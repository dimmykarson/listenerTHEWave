using UnityEngine;
using System.Collections;
using System;
using System.Linq;

public class Desafio : MonoBehaviour {
    [SerializeField]
    private Dificuldade dificuldade;
    private Melodia[] melodias;

    public delegate void CheckDelegate();
    public event CheckDelegate OnChecker;

    public void init()
    {
        Debug.Log("Carregando melodias");
        melodias = GetComponentsInChildren<Melodia>();
    }



    //Checa se as notas em memória são as mesmas da melodia
    //Retorna 0 se não tem as condições necessárias
    //Retorna 1 se as notas batem com a melodia
    //Retorna -1 se não batem na sequência
    public int Checar(ArrayList notas)
    {
        
        if (notas == null) return 0;
        if (notas.Count == 0) return 0;
        int qtMelodias = melodias.Length;
        if (notas.Count != qtMelodias) return 0;
        for(int i = 0; i < qtMelodias; i++)
        {
            if (notas[i] == null) return 0;
            if (!melodias[i].Comparar(Convert.ToString(notas[i])))
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
