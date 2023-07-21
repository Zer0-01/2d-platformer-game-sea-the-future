using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WobblyText : MonoBehaviour
{

    public TMP_Text textComponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textinfo = textComponent.textInfo;

        for (int i=0; i<textinfo.characterCount; ++i)
        {
            var charInfo = textinfo.characterInfo[i];

            if (!charInfo.isVisible)
                continue;

            var verts = textinfo.meshInfo[charInfo.materialReferenceIndex].vertices;

            for (int j=0; j<4; ++j)
            {
                var orig = verts[charInfo.vertexIndex + j];
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time*2f + orig.x*0.01f) * 13f);
            }

        }

        for (int i=0; i<textinfo.meshInfo.Length; ++i)
        {
            var meshInfo = textinfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh, i);
        }
    }
}
