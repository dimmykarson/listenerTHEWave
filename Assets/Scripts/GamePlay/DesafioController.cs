using UnityEngine;
using System.Collections;

public class DesafioController : MonoBehaviour {
    protected Desafio[] desafios;
    protected float quantidadeDesafios = 0;
	
	void Start () {
        desafios = GetComponentsInChildren<Desafio>();
        quantidadeDesafios = desafios.Length;
	}
	
	public Desafio getDesafio()
    {
        int pos = (int)Random.Range(0f, quantidadeDesafios);
        return desafios[pos];
    }
}
