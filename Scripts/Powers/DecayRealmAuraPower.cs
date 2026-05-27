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

public class DecayRealmAuraPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff; // 假设是Debuff，因为减少属性
    public override PowerStackType StackType => PowerStackType.Counter;

    // 自定义图标路径（需要添加图片）
    public override string? CustomPackedIconPath => "res://axeboy/images/powers/decay_realmAura_power.png";
    public override string? CustomBigIconPath => "res://axeboy/images/powers/decay_realmAura_power.png";

}