using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BotoesDeRespostaController : MonoBehaviour {

    private Button[] botoes;

    public Button[] Botoes
    {
        get
        {
            return botoes;
        }

        set
        {
            botoes = value;
        }
    }

    public void Init()
    {
        botoes = GetComponentsInChildren<Button>();
    }

   
    

}
