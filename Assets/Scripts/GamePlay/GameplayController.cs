using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Linq;

public class GameplayController : MonoSingleton<GameplayController>
{
    [SerializeField]
    private Text notaSelecionada;
    [SerializeField]
    private Button tocar;
    [SerializeField]
    private Button btPular;
    [SerializeField]
    private Button iniciar;
    [SerializeField]
    private Button limpar;
    [SerializeField]
    private DesafioController desafioController;
    [SerializeField]
    private ScoreController scoreController;
    [SerializeField]
    private StarController starController;
    [SerializeField]
    private BotoesDeRespostaController botoesController;
    private ArrayList respostas;
    private int indice = 0;
    private bool pular = false;
    private bool tempoAcabou = false;
    [SerializeField]
    private Text mensagem;
    private Nota[] fields;
    private Timer timer;

    private Button[] btNotas;
    private bool pausado = false;
    private int qtDeMelodias = 0;
    [SerializeField]
    private Dialog dialog;

    void Start()
    {
        desafioController.init();
        desafioController.OnDesafioLoad += OnLoadDesafio;
        timer = GetComponentInChildren<Timer>();
        timer.OnDesafioFinish += OnFinishDesafio;

        btPular.onClick.AddListener(PularDesafio);
        iniciar.onClick.AddListener(ReiniciarDesafio);
        limpar.onClick.AddListener(LimparResposta);
       
        respostas = new ArrayList();
        fields = Enum.GetValues(typeof(Nota))
                    .Cast<Nota>()
                    .ToArray();
    }

    void Destroy()
    {
        desafioController.OnDesafioLoad -= OnLoadDesafio;
    }

    private void OnFinishDesafio()
    {
        Debug.Log("Acabou o tempo");
        timer.Contar = false;
        dialog.OnEnable();
    }

    private void OnLoadDesafio()
    {
        Debug.Log("Gerando novo desavio");
        respostas = new ArrayList();
        qtDeMelodias = desafioController.Desafio.Melodias.Length;
        carregarBotoesNotas();
        tocar.onClick.RemoveAllListeners();
        tocar.onClick.AddListener(TocarMusica);
    }

    private void TocarMusica()
    {
        Debug.Log("Tocando música");
        if (starController.Chance <= 0)
        {
            MudarMensagem("Você não pode mais tocar a melodia, agora é acertar!");
            return;
        }
        indiceATocar = 0;
        Chamar();
        starController.DecrementarChances();
    }
    private int indiceATocar = 0;
    public void Chamar()
    {
        if(indiceATocar >= desafioController.Desafio.Melodias.Length)
        {
            return;
        }
        Melodia melodia = desafioController.Desafio.Melodias[indiceATocar];
        melodia.Play();
        Debug.Log(Time.deltaTime + ". Tocando " + melodia.ObterNota());
        if(indiceATocar< desafioController.Desafio.Melodias.Length)
        {
            indiceATocar++;
            Invoke("Chamar", 2.2f);
        }
        
    }

    private IEnumerator Esperar()
    { 
        yield return new WaitForSeconds(3f);
    }

    void Update()
    {
        if (!desafioController.Desafio)
        {
            desafioController.getDesafio();
        }
        if(qtDeMelodias == respostas.Count)
        {
            if (verificarRespostas())
            {
                Debug.Log("Acertou o desafio");
                scoreController.ChangeValue((int)desafioController.Desafio.Dificuldade);
                desafioController.Desafio = null;
            }
            else
            {
                Debug.Log("Errou o desafio");
                desafioController.Desafio = null;
            }
        }
    }

    private bool verificarRespostas()
    {
        switch (desafioController.Desafio.Checar(respostas))
        {
            case -1:
                MudarMensagem("Que pena, você errou! Clique no botão para ouvir outra música e tentar novamente");
                return false;
            case 1:
                MudarMensagem("Parabéns, você acertou! Um novo desafio foi criado!");
                return true;
        }

        return false;
    }

    public void ReiniciarDesafio()
    {
        dialog.OnDisable();
        desafioController.Desafio = null;
        scoreController.Resetar();
        starController.Resetar();
        timer.Restart();
    }

    private void PularDesafio()
    {
        Debug.Log("Pulando botões");
        desafioController.Desafio = null;
        scoreController.ChangeValue(-5);
        starController.Resetar();
    }

    private void LimparResposta()
    {
        Debug.Log("Limpando botões");
        respostas = new ArrayList();
        indice = 0;
    }

    private void carregarBotoesNotas()
    {
       botoesController.Init();
       Debug.Log("Carregando botões");
       Button[] buttons = botoesController.Botoes;
       int qtBotoes = buttons.Length;
       ArrayList listaAleatoriaDeNotas = montarListaAleatoriaDeNotas(qtBotoes);
       for(int i = 0; i < buttons.Length; i++)
       {
            Button b = buttons[i];
            Text t = b.GetComponentsInChildren<Text>()[0];
            t.text = Convert.ToString(listaAleatoriaDeNotas[i]);
            b.onClick.RemoveAllListeners();
            b.onClick.AddListener(() => SetarRespostaNaFila(t.text));
        }
    }

    private void SetarRespostaNaFila(string text)
    {
        if (timer.Contar)
        {
            Debug.Log("Colocando " + text + " na fila de respostas");
            respostas.Add(text);
        }
        else
        {
            Debug.Log("Tempo finalizado, não pode mais!");
        }
        
    }

    private ArrayList montarListaAleatoriaDeNotas(int qt)
    {
        Debug.Log("Montando lista aleatória");
        int qtMelodias = desafioController.Desafio.Melodias.Length;
        ArrayList notas = new ArrayList();
        Debug.Log("Adicionando respostas");
        for (int i = 0; i < qtMelodias; i++)
        {
            notas.Add(desafioController.Desafio.Melodias[i].ObterNota());
        }
        int qtRestante = qt - qtMelodias;
        Debug.Log("Adicionando "+qtRestante+" respostas aleatórias");
        for (int i = 0; i < qtRestante;i++)
        {
            notas.Add(notaAleatoria(notas));
        }
        var rand = new System.Random();
        notas = KLUtil.Shuffle(rand, notas.ToArray());
        return notas;
    }

    private string notaAleatoria(ArrayList notas)
    {
        int rand = (int)UnityEngine.Random.Range(0f, fields.Length);
        Nota nota = fields[rand];
        string notaFinal = nota.ToString();
        notaFinal = notaFinal.Replace("Sus", "#").Replace("b", "º").Replace("Plus", "+");
        for (int i = 0; i < notas.Count; i++)
        {
            if (notaFinal.Equals(notas[i]))
            {
                return notaAleatoria(notas);
            }
        }
        return notaFinal;
    }

    public void MudarMensagem(String msg)
    {
        mensagem.text = msg;
    }
}
