using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SLevelPanel : MonoBehaviour
{
    public SCheckWindow checkWindow;
    public Button closeButt;
    public GameObject boxPanel;
    public TextMeshProUGUI starsCountToUnblock;
    public SBoxPanel sBoxPanel;

    private float prog;
    private float step = 0.01f;
    private Vector2 start;
    private Vector2 end;
    private float scrollDist = 333;
    private int pageNumber;

    private void Start()
    {
        start = boxPanel.transform.position;
        end = boxPanel.transform.position;
    }
    private void Update()
    {
        starsCountToUnblock.text = $"left for next unlock: {sBoxPanel.StarsNumToUnblockNextSet}";
    }
    private void FixedUpdate()
    {
        start = boxPanel.transform.position;
        boxPanel.transform.position = Vector2.Lerp(start, end, prog);
        prog += step;
    }


    public void LeftButt()
    {
        if (pageNumber > 0)
        {
            end.x += scrollDist;
            ResetMove();
            pageNumber--;
        }
    }
    public void RightButt()
    {
        if (pageNumber < 3)
        {
            end.x -= scrollDist;
            ResetMove();
            pageNumber++;
            Debug.Log("ss");
        }
    }
    void ResetMove() => prog = 0;
}