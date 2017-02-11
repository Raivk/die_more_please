using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour
{
    float shakeAmt = 0;

    public Camera mainCamera;

    IEnumerator kick(float intensity, Vector2 direction)
    {
        Vector3 pp = mainCamera.transform.position;
        Vector2 dir = direction / direction.magnitude;
        for (int i = 0; i < 4; i++)
        {
            pp.x += (shakeAmt * 2 - shakeAmt) * dir.x;
            pp.y += (shakeAmt * 2 - shakeAmt) * dir.y;
            mainCamera.transform.position = pp;
            yield return new WaitForSeconds(0.005f);
        }
        //for (int i = 0; i < 2; i++)
        //{
        //    pp.x -= (shakeAmt * 2 - shakeAmt) * dir.x;
        //    pp.y -= (shakeAmt * 2 - shakeAmt) * dir.y;
        //    mainCamera.transform.position = pp;
        //    yield return new WaitForSeconds(0.005f);
        //}
    }

    public void StartShake(int intensity, float duration)
    {
        shakeAmt = intensity * .0025f;
        InvokeRepeating("CameraShake", 0, 0.01f);
        Invoke("StopShaking", duration);
    }

    public void StartKick(float intensity, Vector2 direction)
    {
        shakeAmt = intensity * 0.0025f;
        StartCoroutine(kick(intensity, direction));
    }

    public void setCamera(Camera c)
    {
        this.mainCamera = c;
    }

    void CameraShake()
    {
        if (shakeAmt > 0)
        {
            float quakeAmt = Random.value * shakeAmt * 2 - shakeAmt;
            Vector3 pp = mainCamera.transform.position;
            switch (Random.Range(1, 3))
            {
                case 1:
                    pp.x += quakeAmt;
                    break;
                case 2:
                    pp.y += quakeAmt;
                    break;
                case 3:
                    pp.x += quakeAmt;
                    pp.y += quakeAmt;
                    break;
            }
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("CameraShake");
        mainCamera.transform.position = new Vector3(0, 0, -10);
    }

}