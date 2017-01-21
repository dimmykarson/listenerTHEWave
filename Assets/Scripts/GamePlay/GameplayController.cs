using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameplayController : MonoSingleton<GameplayController>
{
    [SerializeField]
    private Button tocar;
    [SerializeField]
    private Button btPular;
    [SerializeField]
    private Button iniciar;
    [SerializeField]
    private DesafioController desafioController;
    [SerializeField]
    private ScoreController scoreController;
    [SerializeField]
    private StarController starController;
    private string[] notas;
    private int indice = 0;
    private bool pular = false;
    private bool tempoAcabou = false;
    private Desafio desafio;
    [SerializeField]
    private Text mensagem;

    public Button[] btNotas;


    void Start ()
    {
        ReiniciarDesafio();
    }

    private void ReiniciarDesafio()
    {
        CreateDesafio();
        btNotas = new Button[8];
        MudarMensagem("Desafio recomeçou! Clique no botão para ouvir a música");
        scoreController.Resetar();
        starController.Resetar();
    }

    private void PularDesafio()
    {
        pular = true;
    }

    void Destroy()
    {
        
    }

   

   

    private void CreateDesafio()
    {
        desafio = desafioController.getDesafio();
        notas = new string[desafio.Melodias.Length];
        indice = 0;
        tocar.onClick.AddListener(TocarMusica);
    }

    private void TocarMusica()
    {
        Debug.Log("Tocando música");
        if (tempoAcabou)
        {
            MudarMensagem("Que pena, seu tempo acabou para esse desafio!");
            return;
        }
            if (starController.Chance <= 0)
        {
            MudarMensagem("Você não pode mais tocar a melodia, agora é acertar!");
            return;
        }

        for(int i = 0; i < desafio.Melodias.Length; i++)
        {
            Melodia melodia = desafio.Melodias[i];
            melodia.Play();
            StartCoroutine(Esperar());
        }
        starController.DecrementarChances();
    }

    private IEnumerator Esperar()
    {
        yield return new WaitForSeconds(0.1f);
    }

    void Update () {
        if (pular)
        {
            CreateDesafio();
            MudarMensagem("Eita! Desafio pulado! Clique no botão para ouvir outra música");
            pular = false;
        }
        if (tempoAcabou)
        {
            MudarMensagem("O tempo acabou! Clique em Iniciar para recomeçar o desafio!");
            return;
        }
        switch (desafio.Checar(notas))
        {
            case -1:
                //Errou a melodia,exibir mensagem de erro, pegar um novo desafio
                CreateDesafio();
                MudarMensagem("Eita! Você errou! Clique no botão para ouvir outra música");
                break;
            case 0:
                //Não fazer nada
                break;
            case 1:
                //Acertou, aumentar o score e pegar um novo desafio
                aumentarScore();
                CreateDesafio();
                MudarMensagem("Parabéns! Você acertou! Um novo desafio foi criado!");
                break;
        }
	}

    public void MudarMensagem(String msg)
    {
        mensagem.text = "Desafio criado, clique no botão para ouvir!";
        if (msg != null)
        {
            mensagem.text = msg;
        }
    }

    private void aumentarScore()
    {
        scoreController.ChangeValue((int) desafio.Dificuldade);
    }

    public bool Pular
    {
        get
        {
            return pular;
        }

        set
        {
            pular = value;
        }
    }

    public bool TempoAcabou
    {
        get
        {
            return tempoAcabou;
        }

        set
        {
            tempoAcabou = value;
        }
    }
}
