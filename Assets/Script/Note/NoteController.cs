using UnityEngine;

public class NoteController : MonoBehaviour
{
    [SerializeField] private float beatTempo;

    public bool isStarted;

    private void Update()
    {
        if (!isStarted)
        {
            if (Input.anyKeyDown)
            {
                isStarted = true;
            }
        }
        else
        {
            transform.position -= new Vector3(0, beatTempo * Time.deltaTime, 0);
        }
    }

    private void Start()
    {
        beatTempo = beatTempo / 60f;
    }
}
