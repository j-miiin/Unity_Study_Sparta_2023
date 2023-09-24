using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float time;
    public float fullDayLength;
    public float startTime = 0.4f;
    private float timeRate;
    public Vector3 noon;        // 자정에 각도를 몇 도로 만들 것인가

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity; // 그래프에 맞춰서 원하는 값들을 time 값으로 꺼내올 수 있음

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;  // 환경광
    public AnimationCurve reflectionIntensityMuliplier; // 반사광

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;    // 얼만큼씩 변해야 하는지
        time = startTime;
    }

    private void Update()
    {
        // percentage로 쓸 것이라서 1.0f로 나눠줌 
        // time은 0~0.999
        time = (time + timeRate * Time.deltaTime) % 1.0f;   

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        // 위에는 빛만 바뀌는 것이므로
        // 프로젝트의 전체적인 환경을 바꿔줘야 함
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMuliplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);    // 시간에 맞는 그래프 값을 가져옴

        // 360도를 기준으로 해가 뜨는 위치와 달이 뜨는 위치를 잡아줌
        // 해가 뜨는 위치 : 0.25 / 달이 뜨는 위치 : 0.75
        // x,y축 위에 원을 4등분해서 생각 (noon에 해는 y축에서 내리쬐게 되므로 1/4 지점에서 해가 뜸
        // 달은 3/4 지점에서 해가 뜨게 되므로 0.75 (달이 제일 꼭대기에 있을 때)
        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * noon * 4.0f;
        lightSource.color = colorGradient.Evaluate(time);
        lightSource.intensity = intensity;

        GameObject go = lightSource.gameObject;
        if (lightSource.intensity == 0 && go.activeInHierarchy)
        {
            go.SetActive(false);
        } else if (lightSource.intensity > 0 && !go.activeInHierarchy)
        {
            go.SetActive(true);
        }
    }
}
