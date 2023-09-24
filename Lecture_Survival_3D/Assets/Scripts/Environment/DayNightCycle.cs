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
    public Vector3 noon;        // ������ ������ �� ���� ���� ���ΰ�

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity; // �׷����� ���缭 ���ϴ� ������ time ������ ������ �� ����

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;  // ȯ�汤
    public AnimationCurve reflectionIntensityMuliplier; // �ݻ籤

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;    // ��ŭ�� ���ؾ� �ϴ���
        time = startTime;
    }

    private void Update()
    {
        // percentage�� �� ���̶� 1.0f�� ������ 
        // time�� 0~0.999
        time = (time + timeRate * Time.deltaTime) % 1.0f;   

        UpdateLighting(sun, sunColor, sunIntensity);
        UpdateLighting(moon, moonColor, moonIntensity);

        // ������ ���� �ٲ�� ���̹Ƿ�
        // ������Ʈ�� ��ü���� ȯ���� �ٲ���� ��
        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMuliplier.Evaluate(time);
    }

    void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);    // �ð��� �´� �׷��� ���� ������

        // 360���� �������� �ذ� �ߴ� ��ġ�� ���� �ߴ� ��ġ�� �����
        // �ذ� �ߴ� ��ġ : 0.25 / ���� �ߴ� ��ġ : 0.75
        // x,y�� ���� ���� 4����ؼ� ���� (noon�� �ش� y�࿡�� �����ذ� �ǹǷ� 1/4 �������� �ذ� ��
        // ���� 3/4 �������� �ذ� �߰� �ǹǷ� 0.75 (���� ���� ����⿡ ���� ��)
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
