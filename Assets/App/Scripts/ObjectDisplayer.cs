using HoloToolkitExtensions.Messaging;
using UnityEngine;

public class ObjectDisplayer : MonoBehaviour
{
    void Start()
    {
        Messenger.Instance.AddListener<PositionFoundMessage>(ShowObject);

#if !UNITY_EDITOR
        gameObject.SetActive(false);
#endif
    }

    private void ShowObject(PositionFoundMessage m)
    {
        if (m.Status == PositionFoundStatus.Accepted)
        {
            transform.position = new Vector3(transform.position.x, m.Location.y,
                transform.parent.transform.position.z) + Vector3.up * 0.05f;
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
