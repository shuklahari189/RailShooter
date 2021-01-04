using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private void Awake()
    {
        int NoOfAudioPlayer = FindObjectsOfType<AudioPlayer>().Length;

        if(NoOfAudioPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
