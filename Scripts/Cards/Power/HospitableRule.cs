using AxeBoy.Scripts.Powers;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class HospitableRule : CustomCardModel
{
    public override bool GainsBlock => true;
    private const int energyCost = 1;
    private const CardType type = CardType.Power;
    private const CardRarity rarity = CardRarity.Uncommon;
    private const TargetType targetType = TargetType.Self;
    public override IEnumerable<CardKeyword> CanonicalKeywords
        => new List<CardKeyword> {CardKeyword.Innate };
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/hospitable_rule.png";
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<HospitableRulePower>(1m),
    ];
    
    public HospitableRule() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int stackCount = base.DynamicVars["HospitableRulePower"].IntValue;
            await PowerCmd.Apply<HospitableRulePower>(
                base.Owner.Creature,
                stackCount,
                base.Owner.Creature,
                null
            );
    }
    protected override void OnUpgrade()
    {
        DynamicVars["HospitableRulePower"].UpgradeValueBy(1);
    }
}
