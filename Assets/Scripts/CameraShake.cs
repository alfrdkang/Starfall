/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 22/06/2024
 * Date Modified: 23/06/2024
 * Description: Script to shake camera.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Alfred Kang Jing Rui
/// Date Created: 22/06/2024
/// Date Modified: 23/06/2024
/// Description: Script to shake the camera by applying random offsets to its position.
/// </summary>
public class CameraShake : MonoBehaviour
{
    /// <summary>
    /// Coroutine to shake the camera for a specified duration and magnitude.
    /// </summary>
    /// <param name="duration">The duration of the shake effect.</param>
    /// <param name="magnitude">The magnitude of the shake effect.</param>
    /// <returns></returns>
    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = transform.localPosition;

        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = originalPos;
    }
}
