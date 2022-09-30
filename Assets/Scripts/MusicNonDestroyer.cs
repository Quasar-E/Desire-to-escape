using UnityEngine;

public class MusicNonDestroyer : MonoBehaviour
{
    private void Awake() 
    {
        GameObject[] musicObjs = GameObject.FindGameObjectsWithTag("Music");       

        if (musicObjs.Length > 1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }
}
