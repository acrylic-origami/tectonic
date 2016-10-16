using UnityEngine;
using System.Collections;

public class SemiSigmoid {
    float y0 = 0.5f, A = 1.0f, tau = Mathf.Exp(-1);
    public SemiSigmoid(float y0, float A, float tau) {
        this.tau = tau;
        this.A = A;
        this.y0 = y0;
    }
    public SemiSigmoid(float y0, float A) {
        this.A = A;
        this.y0 = y0;
    }
    public SemiSigmoid() {}
    public float eval(float x) {
        return (float)(this.y0 - 0.5) + A / (1 + Mathf.Exp(-x/this.tau));
    }
}
