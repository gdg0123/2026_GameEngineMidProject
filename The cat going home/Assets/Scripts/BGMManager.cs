using UnityEngine;
using UnityEngine.SceneManagement;


public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;

    public AudioClip bgm;
    public AudioClip bgm2;

    private AudioSource audioSource;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void Awake()
    {

        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }



        audioSource = GetComponent<AudioSource>();
        audioSource.clip = bgm;
        audioSource.loop = true;
        audioSource.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Level_2")
        {
            if(audioSource.clip == bgm2 && audioSource.isPlaying)
            {
                return;
            }
            audioSource.clip = bgm2;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            if (audioSource.clip == bgm && audioSource.isPlaying)
            {
                return;
            }
            audioSource.clip = bgm;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
