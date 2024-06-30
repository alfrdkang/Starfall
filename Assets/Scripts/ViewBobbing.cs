/*
 * Author: Alfred Kang Jing Rui
 * Date Created: 23/06/2024
 * Date Modified: 23/06/2024
 * Description: Scripts that handles player view bobbing
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles view bobbing effect based on player movement input.
/// </summary>
[RequireComponent(typeof(PositionFollower))]
public class ViewBobbing : MonoBehaviour
{
    [Header("Bobbing Settings")]
    [SerializeField] private float EffectIntensity; // Intensity of the bobbing effect
    [SerializeField] private float EffectIntensityX; // Intensity of the bobbing effect in the X direction
    [SerializeField] private float EffectSpeed; // Speed of the bobbing effect

    private PositionFollower FollowerInstance; // Reference to the PositionFollower component
    private Vector3 OriginalOffset; // Original offset of the position follower
    private float SinTime; // Time variable used for sine wave calculation

    // Start is called before the first frame update
    private void Awake()
    {
        FollowerInstance = GetComponent<PositionFollower>(); // Get PositionFollower component attached to the same GameObject
        OriginalOffset = FollowerInstance.Offset; // Store original offset of the position follower
    }

    // Update is called once per frame
    private void Update()
    {
        Vector3 inputVector = new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal")); // Get player movement input

        if (inputVector.magnitude > 0f)
        {
            SinTime += Time.deltaTime * EffectSpeed; // Increment SinTime based on EffectSpeed if there's player movement
        }
        else
        {
            SinTime = 0f; // Reset SinTime to 0 if there's no player movement
        }

        float sinAmountY = -Mathf.Abs(EffectIntensity * Mathf.Sin(SinTime)); // Calculate vertical bobbing amount using sine wave
        Vector3 sinAmountX = FollowerInstance.transform.right * EffectIntensity * Mathf.Cos(SinTime) * EffectIntensityX; // Calculate horizontal bobbing amount

        // Apply bobbing offsets to the PositionFollower component's offset
        FollowerInstance.Offset = new Vector3
        {
            x = OriginalOffset.x,
            y = OriginalOffset.y + sinAmountY,
            z = OriginalOffset.z
        };

        FollowerInstance.Offset += sinAmountX; // Apply horizontal bobbing offset
    }
}
