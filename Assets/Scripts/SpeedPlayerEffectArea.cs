using UnityEngine;

public class SpeedPlayerEffectArea : PlayerEffectArea
{
    public float speedMultiplier = 2f;

    private void ChangeSpeed(GameObject player, float multiplier)
    {
        player.GetComponent<PhysicalCharacterController2D>().moveSpeed *= multiplier;
    }

    protected override void OnPlayerEnter(GameObject player)
    {
        ChangeSpeed(player, speedMultiplier);
    }

    protected override void OnPlayerExit(GameObject player)
    {
        ChangeSpeed(player, 1 / speedMultiplier);
    }
}
