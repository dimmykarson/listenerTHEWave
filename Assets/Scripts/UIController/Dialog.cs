using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialog : MonoBehaviour {

    void Start()
    {
        OnDisable();
    }

    public void OnDisable()
    {
        transform.SetAsFirstSibling();
    }

    public void OnEnable()
    {
        transform.SetAsLastSibling();  //This is very, very simple.  It takes the Model Panel and changes the
    }
}
