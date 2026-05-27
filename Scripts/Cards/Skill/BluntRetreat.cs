using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class BluntRetreat : CustomCardModel
{
    public override bool GainsBlock => true;
    private const int energyCost = 1;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Common;
    private const TargetType targetType = TargetType.AnyEnemy;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/blunt_retreat.png";
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(13, ValueProp.Move),new PowerVar<ResentfulSoulsPower>(3m)];

    public BluntRetreat() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        int stackCount = -base.DynamicVars["ResentfulSoulsPower"].IntValue;
        await PowerCmd.Apply<ResentfulSoulsPower>(
            cardPlay.Target,
            stackCount,
            base.Owner.Creature,
            null
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ResentfulSoulsPower"].UpgradeValueBy(-1);
    }
}