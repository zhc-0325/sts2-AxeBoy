using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;
using System.Collections;

// 怨灵鬼火 自定义Debuff
public class ResentfulSoulsPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;

    // 自定义图标路径
    public override string? CustomPackedIconPath => "res://axeboy/images/powers/resentful_souls_power.png";
    public override string? CustomBigIconPath => "res://axeboy/images/powers/resentful_souls_power.png";

    // 定义基础数值：基础1.1倍率
    protected override IEnumerable<DynamicVar> CanonicalVars => new DynamicVar[]
    {
        new DynamicVar("BaseMult", 1.1m)
    };

    public override decimal ModifyDamageMultiplicative(
        Creature? target,
        decimal amount,
        ValueProp props,
        Creature? dealer,
        CardModel? cardSource)
    {
        if (target != base.Owner)
        {
            return 1m;
        }

        if (!props.IsPoweredAttack())
        {
            return 1m;
        }

        decimal baseMult = base.DynamicVars["BaseMult"].IntValue;
        decimal totalMult = baseMult + (base.Amount - 1) * 0.1m;
        DebilitatePower debilitate = target.GetPower<DebilitatePower>();
        if (debilitate != null)
        {
            totalMult = debilitate.ModifyVulnerableMultiplier(target, totalMult, props, dealer, cardSource);
        }
        return totalMult;
    }
}