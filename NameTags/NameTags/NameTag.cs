using NameTags.Assets;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace NameTags
{
    public class NameTag : MonoBehaviour
    {
        string playerNickName;
        Color playerColor;
        VRRig vrrig;
        GameObject nametag;
        GameObject text;
        GameObject blob;
        TextMeshPro textM;
        Renderer Renderer;


        float yOffset = 0.5f;
        float BlobAlpha = 0.05f;
        float TextAlpha = 0.7f;

        private void Awake()
        {
            vrrig = GetComponent<VRRig>();


            // Spawn the nametag
            Spawn();
        }



        private void Update() {

            try
            {
                if (PhotonNetwork.InRoom && vrrig.gameObject.activeInHierarchy && nametag == null)
                {
                    // Joined a lobby, creating the name tag!
                    Spawn();

                }
                if (PhotonNetwork.InRoom == false)
                {
                    // You left the lobby, Destroying the name tag!
                    Destroy(nametag);
                }

                if (vrrig != null && nametag != null && PhotonNetwork.InRoom)
                {
                    // Calling the name tag loop!
                    NameTagLoop();
                }
            }
            catch (Exception e) {

                Debug.LogError($"An error has occured in the name tag mod! Report this to czangilos! {e.Message}");

            }

        }

        // Recursivly finding child of transform with the specified name!
        private GameObject FindChildByName(Transform parent, string name)
        {
            foreach (Transform child in parent)
            {
                if (child.name == name)
                {
                    return child.gameObject;
                }
                GameObject found = FindChildByName(child, name);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }
        // Spawns the name tag!
        private void Spawn()
        {
            nametag = Instantiate(AssetRef.Tag);
            if (nametag != null)
            {
                nametag.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
                text = FindChildByName(nametag.transform, "NameTagText");
                if (text != null)
                {
                    blob = FindChildByName(text.transform, "NameTagBlob");
                    if (blob != null)
                    {
                        textM = text.GetComponent<TextMeshPro>();
                        Renderer = blob.GetComponent<Renderer>();
                    }
                }


            }
        }
        private void OnDisable()
        {
            // Somebody left the code, destroying it's name tag!
            Destroy(nametag);
        }

        private void NameTagLoop()
        {
            // Name tag code
            nametag.transform.LookAt(GorillaTagger.Instance.offlineVRRig.transform.position);

            textM.text = vrrig.playerNameVisible;


            if (vrrig.mainSkin.material.name.Contains("fected") == false)
            {

                playerColor = vrrig.materialsToChangeTo[vrrig.currentMatIndex].color;

                Color textColor = new Color(playerColor.r, playerColor.g, playerColor.b, TextAlpha);
                textM.color = textColor;

                Color blobColor = new Color(playerColor.r, playerColor.g, playerColor.b, BlobAlpha);
                Renderer.material.color = blobColor;
            }
            else
            {

                Color textColor = new Color(0.9607843f, 0.345098f, 0, TextAlpha);
                textM.color = textColor;

                Color blobColor = new Color(0.9607843f, 0.345098f, 0, BlobAlpha);
                Renderer.material.color = blobColor;
            }




            Vector3 playerPos = vrrig.transform.position;
            nametag.transform.position = new Vector3(playerPos.x, playerPos.y + yOffset, playerPos.z);

            blob.transform.position = text.transform.position;
            blob.transform.localScale = new Vector3(textM.preferredWidth, textM.preferredHeight, blob.transform.localScale.z);
        }
    }
}
