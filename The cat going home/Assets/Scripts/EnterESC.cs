using UnityEngine;

public class EnterESC : MonoBehaviour
{
    public GameObject menuPanel;

    void Start()
    {
        menuPanel.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuPanel.SetActive(true);
        }

        
    }
}
