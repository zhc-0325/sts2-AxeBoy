using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;
using System.Collections;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands.Builders;

namespace AxeBoy.Scripts.Powers;

public class HospitableRulePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override bool IsInstanced => true;

    public override string? CustomPackedIconPath => "res://axeboy/images/powers/hospitable_rule_power.png";
    public override string? CustomBigIconPath => "res://axeboy/images/powers/hospitable_rule_power.png";
    public override async Task AfterAttack(AttackCommand command)
    {
        if (command.Attacker != base.Owner)
        {
            return;
        }
        List<DamageResult> list = command.Results.ToList();
        List<DamageResult> petHits = list.Where(r => r.Receiver.IsPet).ToList();
        foreach (DamageResult petHit in petHits)
        {
            list.RemoveAll(r => r.Receiver == petHit.Receiver.PetOwner?.Creature);
        }
        int hitCount = list.Count(r => r.UnblockedDamage > 15);
        if (hitCount > 0)
        {
            Flash();
            await PowerCmd.Apply<VisibilityPower>(base.Owner, base.Amount * hitCount, base.Owner, null);
        }
    }
}