using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Typewriter : MonoBehaviour
{

    public static void Type(TextMeshProUGUI tmp, string text)
    {
        if (tmp == null) return;
        var typewriter = tmp.GetComponent<Typewriter>() ?? tmp.gameObject.AddComponent<Typewriter>();
        typewriter.TypeText(text);
    }

    private const int CharactersPerSecond = 100;
    private const float AudioVolume = 0.25f;
    private const string AudioClipName = "Typewriter";

    private TextMeshProUGUI tmp;
    private AudioSource audioSource;

    private void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.volume = AudioVolume;
        audioSource.clip = Resources.Load<AudioClip>(AudioClipName);
    }

    private void TypeText(string text)
    {
        StopAllCoroutines();
        StartCoroutine(TypeCharacters(text));
    }

    private IEnumerator TypeCharacters(string text)
    {
        tmp.text = text;
        tmp.ForceMeshUpdate();
        tmp.maxVisibleCharacters = 0;
        float elapsed = 0;
        while (tmp.maxVisibleCharacters < tmp.textInfo.characterCount)
        {
            tmp.maxVisibleCharacters = Mathf.CeilToInt(elapsed * CharactersPerSecond);
            audioSource.Play();
            tmp.maxVisibleCharacters++;
            yield return null;
            elapsed += Time.deltaTime;
        }
    }

}
