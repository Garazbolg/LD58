using UnityEngine;

public static class AnimationCurveExtra
{
    public static AnimationCurve InOut(float startValue, float endValue)
    {
        AnimationCurve curve = new AnimationCurve();
        curve.AddKey(0f, startValue);
        curve.AddKey(0.5f, (startValue + endValue) / 2);
        curve.AddKey(1f, endValue);
        return curve;
    }
    
    public static AnimationCurve FastIn(
        float timeStart,
        float valueStart,
        float timeEnd,
        float valueEnd)
    {
        return (double) timeStart == (double) timeEnd ? new AnimationCurve(new Keyframe[1]
        {
            new Keyframe(timeStart, valueStart)
        }) : new AnimationCurve(new Keyframe[2]
        {
            new Keyframe(timeStart, valueStart, 2,2),
            new Keyframe(timeEnd, valueEnd, 0.0f, 0.0f)
        });
    }
    
    public static AnimationCurve SlowIn(
        float timeStart,
        float valueStart,
        float timeEnd,
        float valueEnd)
    {
        return (double) timeStart == (double) timeEnd ? new AnimationCurve(new Keyframe[1]
        {
            new Keyframe(timeStart, valueStart)
        }) : new AnimationCurve(new Keyframe[2]
        {
            new Keyframe(timeStart, valueStart, 0.0f, 0f),
            new Keyframe(timeEnd, valueEnd, 1f, 0.0f)
        });
    }
}