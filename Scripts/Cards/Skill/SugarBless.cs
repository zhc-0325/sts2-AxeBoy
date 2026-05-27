using AxeBoy.Scripts.Powers;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.Models.Powers;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class SugarBless : CustomCardModel
{
    private const int energyCost = 0;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Common;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/sugar_bless.png";

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<VisibilityPower>()
    };

    public SugarBless() : base(energyCost, type, rarity, TargetType.Self, shouldShowInCardLibrary)
    {
    }
     protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VisibilityPower>(1m),
        new EnergyVar(1)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var visibilityPower = Owner.Creature.GetPower<VisibilityPower>();
        if (visibilityPower != null &&visibilityPower.Amount >= DynamicVars["VisibilityPower"].IntValue)
        {
            await PlayerCmd.GainEnergy(base.DynamicVars.Energy.IntValue, base.Owner);
            await PowerCmd.Apply<VisibilityPower>(base.Owner.Creature,-DynamicVars["VisibilityPower"].IntValue,base.Owner.Creature,this);
        }

        await PowerCmd.Apply<EnergyNextTurnPower>(base.Owner.Creature,1,base.Owner.Creature,this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["VisibilityPower"].UpgradeValueBy(-1); 
        CardCmd.ApplyKeyword(this, CardKeyword.Retain);
    }
}