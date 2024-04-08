using System.Collections;
using UnityEngine;


public class SuperJumpItem : BaseItem
{
    private float superJumpDelay = 3f;
    private float superJumpPower = 5f;


    private void Awake()
    {
        ItemName = "슈퍼 점프";
        ItemPrice = 500f;
        ItemDescription = $"{superJumpDelay:0}초 뒤 강력한 점프를 시전합니다.";
    }

    public override void UseItem(PlayerController player)
    {
        Debug.LogWarning("SuperJumpItem.UseItem()");

        StartCoroutine(SuperJumpCoroutine(player));
    }

    private IEnumerator SuperJumpCoroutine(PlayerController player)
    {
        yield return new WaitForSeconds(superJumpDelay);

        player.rb.AddForce(player.JumpPower * superJumpPower * Vector3.up);
    }
}
