using UnityEngine;
using TMPro;

public class LochTrigger : MonoBehaviour
{
    public TMP_Text text;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball"))
            return;

        if (text != null)
            text.text = "GELOCHT: " + other.name;

        other.gameObject.SetActive(false);
    }
}
