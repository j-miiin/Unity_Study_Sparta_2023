using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target;           // 카메라가 따라갈 대상
    public float moveSpeed;             // 카메라가 따라갈 속도
    private Vector3 targetPosition;     // 대상의 현재 위치

    private string FOLDER_PATH = $"{Application.dataPath}/ScreenShots/";
    private string TOTAL_PATH => $"{FOLDER_PATH}/MyScreenShot_{DateTime.Now.ToString("MMdd_HHmmss")}.png";

    void Update()
    {
        if (target.gameObject != null)
        {
            // this는 카메라
            // Z 값은 카메라 값을 그대로 유지
            targetPosition.Set(target.transform.position.x, target.transform.position.y, this.transform.position.z);

            // vectorA -> B까지 T의 속도로 이동
            this.transform.position = Vector3.Lerp(this.transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    // 스크린샷 기능
    public void OnPosterRender()
    {
        Camera.main.orthographicSize = 10f;

        Texture2D screenTex = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        Rect area = new Rect(150, 0f, Screen.width, Screen.height);
        screenTex.ReadPixels(area, 0, 0);

        try
        {
            // 폴더가 존재하지 않으면 새로 생성
            if (Directory.Exists(FOLDER_PATH) == false)
            {
                Directory.CreateDirectory(FOLDER_PATH);
            }

            // 스크린샷 저장
            File.WriteAllBytes(TOTAL_PATH, screenTex.EncodeToPNG());
        } catch (Exception e)
        {
            Debug.Log(e);
        }
        Destroy(screenTex);

        Camera.main.orthographicSize = 5f;
    }
}
