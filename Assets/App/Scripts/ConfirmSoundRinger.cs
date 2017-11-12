
using HoloToolkitExtensions.Messaging;
using UnityEngine;

public class ConfirmSoundRinger : MonoBehaviour
{
    void Start()
    {
        Messenger.Instance.AddListener<ConfirmSoundMessage>(ProcessMessage);
    }

    private void ProcessMessage(ConfirmSoundMessage arg1)
    {
        PlayConfirmationSound();
    }

    private AudioSource _audioSource;

    private void PlayConfirmationSound()
    {
        if (_audioSource == null)
        {
            _audioSource = GetComponent<AudioSource>();
        }
        if (_audioSource != null)
        {
            _audioSource.Play();
        }
    }
}
