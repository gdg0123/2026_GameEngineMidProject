using UnityEngine;
using UnityEngine.Audio;

public class ItemObject : MonoBehaviour
{
    public AudioClip Item1Sound;
    private AudioSource audioSource;
    private static ItemObject instance;

    public void GetItem1()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Item1Sound;
        audioSource.PlayOneShot(Item1Sound);
    }
}
