using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spins the object for 1 second
/// </summary>
public class Spin : MonoBehaviour
{
    public void StartSpin()
    {
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate(float duration = 1)
    {
        float startRotation = transform.eulerAngles.y;
        float endRotation = startRotation + 360.0f;
        float deltaTime = 0.0f;
        while (deltaTime < duration)
        {
            deltaTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotation, deltaTime / duration) % 360.0f;
            transform.eulerAngles = new Vector3(
                transform.eulerAngles.x,
                yRotation,
                transform.eulerAngles.z);

            yield return null;
        }
    }
}
