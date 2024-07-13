using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Hedronizer))]
public class HedronizerRenderer : MonoBehaviour
{

    [SerializeField]
    Hedronizer hedronizer;
    private uint count;
    public Material material;

    // in case some update got lost
    public uint ForceUpdateEveryFrames = 120;

    void Start(){
        hedronizer = GetComponent<Hedronizer>();
        //GraphicsSettings.useScriptableRenderPipelineBatching = false;
        FindAnyObjectByType<VolumeManager>().Updated += UpdateRenderingMesh;
    }

    void Update(){
        if(count % ForceUpdateEveryFrames == 0){
            hedronizer.RunVisualization();
            count = 0;
        }
        count++;
        Draw();
    }


    void UpdateRenderingMesh()
    {
        hedronizer.RunVisualization();
    }

    void Draw() {
        RenderParams rp = new RenderParams(material);

        Vector3 b1 = hedronizer.transform.position;
        Vector3 b2 = hedronizer.size.xyz; 

        rp.worldBounds = new Bounds(b1, b2);
        rp.matProps = new MaterialPropertyBlock();

        rp.matProps.SetBuffer("_Data", hedronizer.computeBuffer);
        rp.matProps.SetMatrix("_ObjectToWorld", transform.localToWorldMatrix);
        Graphics.RenderPrimitives(rp, MeshTopology.Triangles, hedronizer.ItensInBuffer * 3, 1);
        //Graphics.RenderPrimitivesIndirect(rp, MeshTopology.Triangles, hedronizer.ArgsBuffer, 1);

    }


}
