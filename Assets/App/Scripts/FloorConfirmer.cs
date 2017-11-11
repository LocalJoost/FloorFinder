using HoloToolkitExtensions.Messaging;
using UnityEngine;

public class FloorConfirmer : MonoBehaviour
{

    private PositionFoundMessage _lastReceivedMessage;

    public GameObject ConfirmObject;

    // Use this for initialization
    void Start()
    {
        Messenger.Instance.AddListener<PositionFoundMessage>(ProcessMessage);
        Reset();
#if UNITY_EDITOR
        _lastReceivedMessage =  new PositionFoundMessage(new Vector3(0, -1.6f, 0));
        ResendMessage(true);
#endif
    }

    // Update is called once per frame

    public void Reset()
    {
        if(ConfirmObject != null) ConfirmObject.SetActive(false);
        _lastReceivedMessage = null;
    }

    public void Accept()
    {
        ResendMessage(true);
    }

    public void Reject()
    {
        ResendMessage(false);
    }

    private void ResendMessage(bool accepted)
    {
        if (_lastReceivedMessage != null)
        {
            _lastReceivedMessage.Status = accepted ? PositionFoundStatus.Accepted : PositionFoundStatus.Rejected;
            Messenger.Instance.Broadcast(_lastReceivedMessage);
            Reset();
            if( !accepted) PlayConfirmationSound();
        }
    }

    private void ProcessMessage(PositionFoundMessage message)
    {
        _lastReceivedMessage = message;
        if (message.Status != PositionFoundStatus.Unprocessed)
        {
            Reset();
        }
        else
        {
            ConfirmObject.SetActive(true);
            ConfirmObject.transform.position = message.Location + Vector3.up * 0.05f;
        }
    }


    private void PlayConfirmationSound()
    {
        Messenger.Instance.Broadcast(new ConfirmSoundMessage());
    }

}
