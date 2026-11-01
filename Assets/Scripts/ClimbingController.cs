using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Gravity;
using System.Collections.Generic;
using System.Linq;

public class ClimbingController : MonoBehaviour
{
    [SerializeField] CharacterController playerController;
    [SerializeField] GravityProvider gravityProvider;

    int selectedObjectsCounter = 0;
    HashSet<Transform> activeInteractors;
    Dictionary<Transform, Vector3> lastKnownPos;

    void Start()
    {
        activeInteractors = new HashSet<Transform>();
        lastKnownPos = new Dictionary<Transform, Vector3>();
    }

    void Update()
    {
        if(activeInteractors.Count == 1)
        {
            Transform hand = activeInteractors.First();

            Vector3 diff = hand.transform.position - lastKnownPos[hand];
            playerController.Move(-diff);
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
}
