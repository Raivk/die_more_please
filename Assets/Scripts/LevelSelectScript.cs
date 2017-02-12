using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectScript : MonoBehaviour
{

    public AudioSource music;

    IEnumerator changeLev(int level)
    {
        float fadeTime = Camera.main.GetComponent<Fading>().BeginFade(1);
        StartCoroutine(FadeOut(music, fadeTime));
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(level);
    }

    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

        audioSource.Stop();
        audioSource.volume = startVolume;
    }

    public void startGame(int level)
    {
        StartCoroutine(changeLev(level));
    }

    public void quitGame()
    {
        Application.Quit();
    }

}
