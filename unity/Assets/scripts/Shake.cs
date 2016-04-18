using UnityEngine;

public class Shake : MonoBehaviour {
    public AnimationCurve curve;

    float t = 0f;
    public void Update() {
        t += Time.deltaTime;

        Vector2 shake = new Vector2(ShakeIt(10f, t, curve), ShakeIt(10f, t*2, curve));
        Debug.LogFormat("shake: {0}", shake);
        transform.position = shake;
    }

    private float ShakeIt(float shakeDamper, float t, AnimationCurve curve) {
        return Mathf.PerlinNoise(t / shakeDamper, 0f) * curve.Evaluate(t);
    }
}
