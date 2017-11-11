using System.Collections;
using UnityEngine;

public class HorizontalAnimator : MonoBehaviour
{
    public float SpinTime = 2.5f;

    public bool AbsoluteAxis = true;

    // Use this for initialization
    private void Start()
    {
        if (AbsoluteAxis)
        {
            LeanTween.rotateAround(gameObject, Vector3.up, 360, SpinTime).setLoopClamp();
        }
        else
        {
            StartCoroutine(StartDelayed());
        }
    }

    private IEnumerator StartDelayed()
    {
        yield return new WaitForSeconds(0.1f);
        var rotation = Quaternion.AngleAxis(360f / 3.0f, Vector3.up).eulerAngles;
        LeanTween.rotateLocal(gameObject, rotation, SpinTime / 3.0f).setLoopClamp();
    }
}
