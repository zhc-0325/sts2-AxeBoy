using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class Lumber: CustomCardModel
{
    private const int energyCost = 1;
    private const CardType type = CardType.Attack;
    private const CardRarity rarity = CardRarity.Common;
    private const TargetType targetType = TargetType.AnyEnemy;
        protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ResentfulSoulsPower>(2m),
        new DamageVar(12, ValueProp.Move)
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<ResentfulSoulsPower>()
    };
    private const bool shouldShowInCardLibrary = true;
    public Lumber() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }
    public override string PortraitPath => $"res://axeboy/images/cards/lumber.png";

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int stackCount = -base.DynamicVars["ResentfulSoulsPower"].IntValue;
        await DamageCmd.Attack(DynamicVars.Damage.IntValue)
        .FromCard(this)
        .Targeting(cardPlay.Target)
        .Execute(choiceContext);
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