using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
    private Renderer rend;
    private Color originalColor;
    private Vector3 originalScale;
	
	// Update is called once per frame
	void OnSelect() {
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
        Color newColor = originalColor;
        newColor.r /= 2;
        newColor.g /= 2;
        newColor.b /= 2;
        rend.material.color = newColor;
        originalScale = transform.localScale;
        Vector3 newScale = originalScale;
        newScale.y /= 2;
        transform.localScale = newScale;
        StartCoroutine(Restore());
        SendMessage("OnClick");
	}

    IEnumerator Restore()
    {
        yield return new WaitForSeconds(0.3f);
        rend.material.color = originalColor;
        transform.localScale = originalScale;
    }
}
