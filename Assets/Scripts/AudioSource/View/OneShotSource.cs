using UnityEngine;

namespace Assets.Scripts.AudioSource.View
{
    public class OneShotSource : MonoBehaviour
    {
        [SerializeField] private UnityEngine.AudioSource audioSource;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            audioSource.PlayOneShot(audioSource.clip);
            Destroy(gameObject, audioSource.clip.length);
        }
    }
}
