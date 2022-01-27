using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MirrorPortal : MonoBehaviour
{
    [SerializeField] bool isSecond;
    [SerializeField] bool isThird;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            SceneManager.LoadScene(2);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 2 && isSecond)
        {
            SceneManager.LoadScene(3);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 3 && isThird)
        {
            SceneManager.LoadScene(4);
        }
    }
}
