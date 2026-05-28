using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveLoadManager : MonoBehaviour
{
    [Header("Player")]
    public Transform player;

    [Header("Objects to Save (List)")]
    public List<Transform> objectsToSave = new List<Transform>();

    [Header("UI")]
    public Button saveButton;
    public Button loadButton;
    public TextMeshProUGUI statusText;

    private string SavePath => Path.Combine(Application.persistentDataPath, "worlddata.json");

    void Start()
    {
        if (saveButton != null) saveButton.onClick.AddListener(SaveData);
        if (loadButton != null) loadButton.onClick.AddListener(LoadData);

        // 앱 시작 시 자동 로드
        if (File.Exists(SavePath))
        {
            LoadData();
        }
    }

    public void SaveData()
    {
        WorldData data = new WorldData();

        // 플레이어 저장
        data.playerData = new TransformData("Player", player);

        // 오브젝트 리스트 저장
        foreach (Transform obj in objectsToSave)
        {
            if (obj != null)
                data.objectDataList.Add(new TransformData(obj.name, obj));
        }

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(SavePath, json);

        Debug.Log($"[Save] 저장 완료: {SavePath}");
        SetStatus("Save!");
    }

    public void LoadData()
    {
        if (!File.Exists(SavePath))
        {
            Debug.LogWarning("[Load] 저장 파일 없음");
            SetStatus("저장 파일 없음");
            return;
        }

        string json = File.ReadAllText(SavePath);
        WorldData data = JsonUtility.FromJson<WorldData>(json);

        // 플레이어 복원
        if (data.playerData != null && player != null)
        {
            player.position = new Vector3(data.playerData.posX, data.playerData.posY, data.playerData.posZ);
            player.rotation = new Quaternion(data.playerData.rotX, data.playerData.rotY, data.playerData.rotZ, data.playerData.rotW);
        }

        // 오브젝트 리스트 복원
        for (int i = 0; i < data.objectDataList.Count && i < objectsToSave.Count; i++)
        {
            if (objectsToSave[i] == null) continue;
            TransformData td = data.objectDataList[i];
            objectsToSave[i].position = new Vector3(td.posX, td.posY, td.posZ);
            objectsToSave[i].rotation = new Quaternion(td.rotX, td.rotY, td.rotZ, td.rotW);
        }

        Debug.Log("[Load] 불러오기 완료");
        SetStatus("Load!");
    }

    // 앱 종료/일시정지 시 자동 저장
    void OnApplicationQuit() => SaveData();
    void OnApplicationPause(bool pause) { if (pause) SaveData(); }

    void SetStatus(string msg)
    {
        if (statusText != null)
        {
            statusText.text = msg;
            CancelInvoke(nameof(ClearStatus));
            Invoke(nameof(ClearStatus), 2f);
        }
    }

    void ClearStatus()
    {
        if (statusText != null) statusText.text = "";
    }
}