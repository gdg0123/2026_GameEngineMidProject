using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LevelObject : MonoBehaviour
{
    public AudioClip Meow;
    private AudioSource audioSource;
    private static LevelObject instance;

    public string nextLevel;

    public void MoveToNextLevel()
    {

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Meow;
        audioSource.Play();

        SceneManager.LoadScene(nextLevel);


    }
}
