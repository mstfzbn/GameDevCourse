﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Mathf;

public class HealthbarScaler : MonoBehaviour {

    [SerializeField]
    private Transform healthMain = null;

    [SerializeField]
    private Transform healthHighlight = null;

    [SerializeField]
    private Transform hitDisplay = null;

    [SerializeField]
    [Range(1, 5)]
    private float scaleSpeed = 1;

    [SerializeField]
    [Range(0.1f, 1)]
    private float initialWait = 0.1f;

    private float targetScale = 1;
    public float TargetScale {
        get { return targetScale; }
        set {
            targetScale = Clamp01(value);
            healthMain.localScale = new Vector3(targetScale, 1, 1);
            healthHighlight.localScale = healthMain.localScale;
            if (!isScaling) {
                StartCoroutine(ScaleHealthbar());
            }
        }
    }

    private float epsilon = 0.01f;
    private bool isScaling = false;

    private IEnumerator ScaleHealthbar() {
        isScaling = true;
        yield return new WaitForSeconds(initialWait);

        while (Abs(hitDisplay.localScale.x - targetScale) > epsilon) {

            Vector3 scale = hitDisplay.localScale;
            scale.x -= scaleSpeed * Time.deltaTime;
            scale.x = Clamp(scale.x, targetScale, 1);
            hitDisplay.localScale = scale;

            yield return null;
        }
        isScaling = false;
    }
}
