
using UnityEngine;
using TMPro;
using System.Collections;

public class CollectionManager : MonoBehaviour
{
    [SerializeField] private int totalCollectibles = 3;
    [SerializeField] private TMP_Text collectiblesText;
    [SerializeField] private Animator doorAnimator;
    [SerializeField] private TMP_Text doorMessage;
    [SerializeField] private AudioSource doorAudio;

    private int collectedCount = 0;
    private bool doorOpened = false;
    private Coroutine messageCoroutine;

    void Start()
    {
        if (doorMessage != null)
            doorMessage.gameObject.SetActive(false);

        UpdateUI();
    }

    public void AddCollectible()
    {
        collectedCount++;
        UpdateUI();
    }

    public void TryOpenDoor()
    {
        if (doorOpened)
            return;

        if (collectedCount >= totalCollectibles)
        {
            doorOpened = true;

            if (doorAnimator != null)
                doorAnimator.SetTrigger("OpenDoor");

            if (doorAudio != null)
                doorAudio.Play();

            ShowMessage("The door is now open!");
        }
        else
        {
            ShowMessage("Collect all items first!");
        }
    }

    private void UpdateUI()
    {
        if (collectiblesText != null)
            collectiblesText.text =
                "Collected: " + collectedCount + "/" + totalCollectibles;
    }

    public void ShowMessage(string message)
    {
        if (doorMessage == null)
            return;

        if (messageCoroutine != null)
            StopCoroutine(messageCoroutine);

        doorMessage.text = message;
        doorMessage.gameObject.SetActive(true);

        messageCoroutine = StartCoroutine(HideMessage());
    }

    private IEnumerator HideMessage()
    {
        yield return new WaitForSeconds(4f);

        doorMessage.gameObject.SetActive(false);
        messageCoroutine = null;
    }
}
