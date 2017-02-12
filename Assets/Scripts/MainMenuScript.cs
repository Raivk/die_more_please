using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

    public AudioSource music;

    IEnumerator changeLev()
    {
        float fadeTime = Camera.main.GetComponent<Fading>().BeginFade(1);
        StartCoroutine(FadeOut(music, fadeTime));
        yield return new WaitForSeconds(fadeTime);
        SceneManager.LoadScene(1);
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

    public void startGame()
    {
        StartCoroutine(changeLev());
        
    }

    public void quitGame()
    {
        Application.Quit();
    }
	
}
