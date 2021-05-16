using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Web;

public class HighScoreManager : MonoBehaviour
{
    [SerializeField]
    private int score;
    [SerializeField]
    private GameObject ScoreContainer, ScoreView;
    [SerializeField]
    private ScoreText prefab;
    [SerializeField]
    public TMP_InputField inputField;
    [SerializeField]
    private TextMeshProUGUI ScorePreview, StatusText;
    private void Start()
    {
        score = PlayerPrefs.GetInt("score");
        ScorePreview.text = $"Score: {score}";
    }
    public void Submit()
    {
        StartCoroutine(SubmitScore());
    }
    public void Fetch()
    {
        StartCoroutine(FetchScore());
    }
    private IEnumerator SubmitScore()
    {
        StatusText.text = "Uploading...";
        string playerName = inputField.text.Replace(" ", "%20");
        string gameName = Application.productName.Replace(" ", "%20");
        //string url = UnityWebRequest.EscapeURL($);
        using (UnityWebRequest request = new UnityWebRequest($"https://us-central1-highscore-manager.cloudfunctions.net/scores/postScore?game={gameName}&playerName={playerName}&score={score}")) {
            request.method = "POST";
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log("Received: " + request.downloadHandler.text);
                    break;
            }
        }
        yield return FetchScore();
        
    }
    private IEnumerator FetchScore()
    {
        StatusText.text = "Fetching Scores...";
        string gameName = Application.productName.Replace(" ", "%20");
        using (UnityWebRequest request = new UnityWebRequest($"https://us-central1-highscore-manager.cloudfunctions.net/scores/fetchScores?game={gameName}"))
        {
            request.method = "GET";
            request.downloadHandler = new DownloadHandlerBuffer();
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(": Error: " + request.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(": HTTP Error: " + request.error);
                    break;
                case UnityWebRequest.Result.Success:
                    string data = $"{{\"scores\": {request.downloadHandler.text}}}";
                    Debug.Log(data);
                    HighScores recievedScores = JsonUtility.FromJson<HighScores>(data);
                    StatusText.gameObject.SetActive(false);
                    ScoreView.SetActive(true);
                    PriorityQueue<ScoreData, int> sortedScores = recievedScores.ToPriorityQueue();
                    int index = 1;
                    while(sortedScores.Count > 0)
                    {
                        ScoreText text = Instantiate(prefab, ScoreContainer.transform);
                        ScoreData scoreData = sortedScores.Pop();
                        text.Index.text = index.ToString();
                        text.Name.text = scoreData.playerName;
                        text.Score.text = scoreData.score.ToString();
                        index++;
                    }
                    break;
            }
        }
    }
}
[System.Serializable]
public class HighScores
{
    public List<ScoreData> scores;
    public PriorityQueue<ScoreData, int> ToPriorityQueue()
    {
        PriorityQueue<ScoreData, int> sortedScores = new PriorityQueue<ScoreData, int>(int.MinValue);
        foreach(ScoreData score in scores)
        {
            sortedScores.Insert(score, int.MinValue + score.score);
        }
        return sortedScores;
    }
}
[System.Serializable]
public class ScoreData
{
    public string id;
    public string playerName;
    public int score;
}
