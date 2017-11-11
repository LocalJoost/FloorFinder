using UnityEngine;

public class PositionFoundMessage
{
    public Vector3 Location { get; }

    public PositionFoundMessage(Vector3 location)
    {
        Location = location;
    }

    public PositionFoundStatus Status { get; set; }
}

