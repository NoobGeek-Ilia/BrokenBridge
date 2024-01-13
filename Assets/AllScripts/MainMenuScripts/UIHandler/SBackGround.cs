using UnityEngine;

public class SBackGround : MonoBehaviour
{
    public GameObject[] cloud;

    void Update()
    {
        CheckCloudsPosition();
        MoveClouds();
    }
    private void MoveClouds()
    {
        float speed = 1;

        for (int i = 0; i < cloud.Length; i++)
        {
            cloud[i].transform.Translate(Vector2.left * Time.deltaTime * speed);
        }
    }
    private void CheckCloudsPosition()
    {
        float offScreen = -1184f;
        float startPosition = 1930f;

        for (int i = 0; i < cloud.Length; i++)
        {
            if (cloud[i].transform.localPosition.x < offScreen)
                cloud[i].transform.localPosition = new Vector2(startPosition, cloud[i].transform.localPosition.y);
        }
    }
}