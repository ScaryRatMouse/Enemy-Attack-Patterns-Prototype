using UnityEngine;

public class SlipperyPlayerEffectArea : PlayerEffectArea
{
    public float dragMultiplier = 0.2f;

    private void ChangeDrag(GameObject player, float multiplier)
    {
        player.GetComponent<PhysicalCharacterController2D>().drag *= multiplier;
    }

    protected override void OnPlayerEnter(GameObject player)
    {
        ChangeDrag(player, dragMultiplier);
    }

    protected override void OnPlayerExit(GameObject player)
    {
        ChangeDrag(player, 1 / dragMultiplier);
    }
}
