using UnityEngine;
using System.Collections;
using System;

public class DesafioController : MonoBehaviour {

    public delegate void DesafioDelegate();

    public event DesafioDelegate OnDesafioLoad;

    protected Desafio[] desafios;
    private Desafio desafio;
    protected float quantidadeDesafios = 0;

    

    public Desafio init () {
        desafios = GetComponentsInChildren<Desafio>();
        Debug.Log("Quantidade de desafios cadastrada: " +  desafios.Length);
        quantidadeDesafios = desafios.Length;
        for(int i = 0; i < desafios.Length; i++)
        {
            desafios[i].init();

        }
        gerarDesafio();
        return desafio;
    }
	
    public void gerarDesafio()
    {
        Debug.Log("(" + Time.deltaTime + ")" + "Carregando desafio");
        int pos = (int)UnityEngine.Random.Range(0f, quantidadeDesafios);
        desafio = desafios[pos];
    }

	public void getDesafio()
    {
        gerarDesafio();
        DispatchLoadDesafio();
    }

    private void DispatchLoadDesafio()
    {
        if (OnDesafioLoad != null)
        {
            OnDesafioLoad();
        }
    }

    public Desafio Desafio
    {
        get
        {
            return desafio;
        }

        set
        {
            desafio = value;
        }
    }
}
