using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.XR.CoreUtils;

public class ClimbingController : MonoBehaviour
{
    [SerializeField] XROrigin xrOrigin;

    [SerializeField] TextMeshProUGUI debugLabel;
    [SerializeField] Transform mainCamera;
    float timerSnapping = 10.0f;

    [SerializeField] Transform[] hands;
    LayerMask terrainLayer;

    [SerializeField] CharacterController playerController;
    [SerializeField] GravityProvider gravityProvider;

    HashSet<Transform> activeInteractors;
    Dictionary<Transform, Vector3> lastKnownPos;

    void Start()
    {
        activeInteractors = new HashSet<Transform>();
        lastKnownPos = new Dictionary<Transform, Vector3>();

        terrainLayer = LayerMask.GetMask("Terrain");
    }

    void Update()
    {
        if(activeInteractors.Count == 1)
        {
            Transform hand = activeInteractors.First();

            Vector3 diff = hand.transform.position - lastKnownPos[hand];
            playerController.Move(-2 * diff);
            lastKnownPos[hand] = hand.transform.position;
        }
    }

    public void SelectEntered(SelectEnterEventArgs args)
    {
        Transform hand = args.interactorObject.transform;
        activeInteractors.Add(hand);
        lastKnownPos[hand] = hand.position;
        
        gravityProvider.gameObject.SetActive(false);
    }

    public void SelectExit(SelectExitEventArgs args)
    {
        Transform hand = args.interactorObject.transform;
        activeInteractors.Remove(hand);

        if (activeInteractors.Count == 0)
        {
            gravityProvider.gameObject.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        timerSnapping -= Time.deltaTime;
        if (timerSnapping > 0)
        {
            return;
        }

        Vector3 downDir = new Vector3(0, -1, 0);

        debugLabel.text = "";
        foreach(Transform hand in hands)
        {
            RaycastHit hit;
            bool found = Physics.Raycast(hand.position, downDir, out hit, 0.4f, terrainLayer);

            debugLabel.text += hit.normal + "\n";

            if (found && hit.normal.y > 0.95)
            {
                // Snap player position
                debugLabel.text += "hit!\n";
                xrOrigin.MoveCameraToWorldLocation(hit.point + new Vector3(0, 1.7f, 0) + mainCamera.forward * 0.5f);
            }
        }
    }
}
