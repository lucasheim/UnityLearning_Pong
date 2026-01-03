using UnityEngine;

public class Wall : MonoBehaviour
{
    public float CalculateVerticalBoundary()
    {
        return CalculateBoundary(transform.position.y, transform.localScale.y);
    }

    public float CalculateHorizontalBoundary()
    {
        return CalculateBoundary(transform.position.x, transform.localScale.x);
    }

    private float CalculateBoundary(float position, float scale)
    {
        return position - (Mathf.Sign(position) * (scale / 2));
    }
}
