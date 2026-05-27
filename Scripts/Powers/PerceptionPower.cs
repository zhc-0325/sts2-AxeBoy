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

namespace AxeBoy.Scripts.Powers;

// 察觉 Power
public class PerceptionPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    public override string? CustomPackedIconPath => "res://axeboy/images/powers/perception_power.png";
    public override string? CustomBigIconPath => "res://axeboy/images/powers/perception_power.png";

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        if (Owner?.Side != side)
        {
            return;
        }
        await PowerCmd.Apply<StrengthPower>(
            base.Owner,
            -Amount,
            base.Owner,
            null
        );
        await PowerCmd.Apply<DexterityPower>(
            base.Owner,
            -Amount,
            base.Owner,
            null
        );
        await PowerCmd.Remove(this);
    }
}
        