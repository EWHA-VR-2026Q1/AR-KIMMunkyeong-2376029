using System.Collections.Generic;

[System.Serializable]
public class TransformData
{
    public string objectName;
    public float posX, posY, posZ;
    public float rotX, rotY, rotZ, rotW;

    public TransformData(string name, UnityEngine.Transform t)
    {
        objectName = name;
        posX = t.position.x;
        posY = t.position.y;
        posZ = t.position.z;
        rotX = t.rotation.x;
        rotY = t.rotation.y;
        rotZ = t.rotation.z;
        rotW = t.rotation.w;
    }
}

[System.Serializable]
public class WorldData
{
    public TransformData playerData;
    public List<TransformData> objectDataList = new List<TransformData>();
}