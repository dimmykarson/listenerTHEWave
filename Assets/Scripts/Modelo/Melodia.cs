using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Melodia : MonoBehaviour {
    [SerializeField]
    private AudioSource audio;
    public AudioClip impact;
    [SerializeField]
    private Nota nota;
 
    
    public bool Comparar(string s)
    {
        if (s == null) return false;
        string f = nota + "";
        return s.Equals(f);
    }

    public string ObterNota()
    {
        return nota + "";
    }

    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if(audio!=null)
            audio.PlayOneShot(impact, 0.7F);
    }

   
   
}
