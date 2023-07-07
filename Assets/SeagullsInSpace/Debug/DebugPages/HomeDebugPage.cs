using UnityEngine;

public class HomeDebugPage : DebugPage
{
    private Rigidbody2D _rigid;
    private FlightController _flight;

    public override string Header { get; } = "Home";

    public HomeDebugPage()
    {
        _flight = UnityEngine.Object.FindObjectOfType<FlightController>();
        _rigid  = _flight.GetComponent<Rigidbody2D>();
    }

    protected override void RunItems(DebugMenu caller)
    {
        ReadOnly($"Speed: {Vector2.Dot(_rigid.velocity, -_rigid.transform.up)}");
    }
}