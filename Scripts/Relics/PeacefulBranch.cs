using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Entities.Creatures;
using System.Collections.Generic;
using System;
using MegaCrit.Sts2.Core.HoverTips;
using AxeBoy.Scripts.Powers;
using MegaCrit.Sts2.Core.Combat;

[Pool(typeof(AxeBoyRelicPool))]
public class PeacefulBranch : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ResentfulSoulsPower>(1m)
    ];
    public override string PackedIconPath => $"res://axeboy/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    protected override string PackedIconOutlinePath => $"res://axeboy/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    protected override string BigIconPath => $"res://axeboy/images/relics/{Id.Entry.ToLowerInvariant()}.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<ResentfulSoulsPower>()
    };

   public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
    {
        if (side == base.Owner.Creature.Side)
        {
        Flash();
        int stackCount = base.DynamicVars["ResentfulSoulsPower"].IntValue;
            var ownResentful = base.Owner.Creature.GetPower<DecayRealmAuraPower>();
                    if (ownResentful != null && ownResentful.Amount > 0)
                    {
                        stackCount *= ownResentful.Amount+1;
                    }
        await PowerCmd.Apply<ResentfulSoulsPower>(Owner.Creature.CombatState.Enemies,stackCount,base.Owner.Creature,null);
        }
    }
}
