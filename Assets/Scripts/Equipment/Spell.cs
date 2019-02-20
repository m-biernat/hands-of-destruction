using UnityEngine;
using UnityEngine.Networking;

public class Spell : NetworkBehaviour
{
    private Player caster;

    private float damage;

    void OnTriggerEnter(Collider other)
    {
        if (isServer && (other.name != caster.name))
        {
            OnPlayerHit(other);
            Destroy(gameObject);
        }
    }

    private void OnPlayerHit(Collider collider)
    {
        if (collider.tag != "Player") return;

        if (collider.GetComponent<Player>() != null)
        {
            byte hitPlayerTeamID = collider.GetComponent<Player>().teamID;
            byte casterTeamID = caster.teamID;

            if (hitPlayerTeamID != casterTeamID || hitPlayerTeamID == 0)
            {
                Player player = GameManager.GetPlayer(collider.name);
                player.RpcTakeDamage(damage, caster.name);
                Debug.Log("Player " + player.playerName + " hit by "
                    + caster.playerName + " for " + damage);
            }
        }
    }

    public void SetSpell(Player caster, float damage)
    {
        this.caster = caster;
        this.damage = damage;
    }
}
