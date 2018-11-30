using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sky : MonoBehaviour
{
    public float minInterval = 0.4f;
    public float maxInterval = 3f;

    public float minLightningDuration = 0.1f;
    public float maxLightningDuration = 0.3f;

    public Color LightningColor = Color.white;

    private Color originalColor;
    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = _spriteRenderer.color;
    }

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(StartLightningStrikes());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator StartLightningStrikes()
    {
        while (isActiveAndEnabled)
        {
            float interval = Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(interval);

            _spriteRenderer.color = LightningColor;
            float duration = Random.Range(minLightningDuration, maxLightningDuration);
            yield return new WaitForSeconds(duration);

            _spriteRenderer.color = originalColor;
        }
    }
}
