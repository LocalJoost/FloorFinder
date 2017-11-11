using UnityEngine;

public class LookatHorizontalDirection : MonoBehaviour
{
    public float RotateAngle = 180f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPostition = new Vector3(Camera.main.transform.position.x,
                                                transform.position.y,
                                                Camera.main.transform.position.z);
        this.transform.LookAt(targetPostition);
        gameObject.transform.Rotate(Vector3.up, RotateAngle);
    }
}
