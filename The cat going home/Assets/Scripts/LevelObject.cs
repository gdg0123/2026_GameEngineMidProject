using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelObject : MonoBehaviour
{
    public AudioClip Meow;
    private AudioSource audioSource;

    public string nextLevel;


    public void MoveToNextLevel()
    {
        GameObject soundObj = new GameObject("Meow");
        AudioSource source = soundObj.AddComponent<AudioSource>();
        DontDestroyOnLoad(soundObj);

        source.clip = Meow;
        source.Play();

        Destroy(soundObj, Meow.length);

        SceneManager.LoadScene(nextLevel);
    }
}
