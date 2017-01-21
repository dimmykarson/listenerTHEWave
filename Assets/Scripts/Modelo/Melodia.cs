using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Melodia : MonoBehaviour {
    [SerializeField]
    private AudioSource audio;
    public AudioClip impact;
    [SerializeField]
    private Nota nota;
    private string complemento;
    
    public bool Comparar(string s)
    {
        string f = nota + complemento;
        return s.Equals(f);
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
