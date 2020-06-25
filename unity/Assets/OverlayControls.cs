using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using UnityEngine.Networking;

public class OverlayControls : MonoBehaviour
{
    public Transform cameraVR;
    public Transform overlay;

    public GameObject overlayGameObject;
    public bool mipMaps = false;

    private float minDistance = 0.4f;
    private float maxDistance = 1.5f;
    private float distance = 0.5f;
    private bool useSuperSample = false;
    public Transform recenterDummy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadTexture());
    }

    private IEnumerator LoadTexture()
    {
        string textureFilePath = Application.streamingAssetsPath + "/spidermanSingle.jpg";
        OVROverlay overlayComponent = overlayGameObject.GetComponent<OVROverlay>();

        UnityWebRequest www = UnityWebRequestTexture.GetTexture(textureFilePath);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError || (www.error != null))
        {
            Debug.Log("XXX Texture not ready: " + textureFilePath);
            yield break;
        }

        Texture2D loadedTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
        Texture2D pageTexture = new Texture2D(loadedTexture.width, loadedTexture.height, loadedTexture.format, mipMaps);
        pageTexture.LoadImage(www.downloadHandler.data);

        overlayComponent.textures[0] = pageTexture;
        overlayComponent.textures[0].hideFlags = HideFlags.HideAndDontSave;
        Debug.Log("XXX TEXTURE " + overlayComponent.textures[0].width + " " + overlayComponent.textures[0].height);
        Recenter();
        www.Dispose();
    }

    private void Recenter()
    {
        transform.localPosition = cameraVR.TransformPoint(new Vector3(0, 0, 0.01f));
        transform.rotation = Quaternion.LookRotation(transform.localPosition - cameraVR.position);

        overlay.localPosition = new Vector3(0, 0, distance);
    }

    private void Zoom(float delta)
    {
        distance += delta;
        overlay.localPosition = new Vector3(0, 0, distance);
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickDown))
        {
            if (distance < minDistance) { return; }
            Zoom(-0.01f);
            return;
        }
        else if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp) || OVRInput.Get(OVRInput.Button.SecondaryThumbstickUp))
        {
            if (distance > maxDistance) { return; }
            Zoom(0.01f);
            return;
        }

        if (OVRInput.GetDown(OVRInput.RawButton.A))
        {
            useSuperSample = !useSuperSample;
            overlayGameObject.GetComponent<OVROverlay>().useExpensiveSuperSample = useSuperSample;
            Debug.Log("XXX Super Sample " + useSuperSample);
            return;
        }

        // Recenter.
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick) || OVRInput.GetDown(OVRInput.Button.SecondaryThumbstick))
        {
            Recenter();
            return;
        }
    }
}
