using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;


namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class BloodSwap : CustomCardModel
{
    private const int energyCost = 0;
    private const CardType type = CardType.Attack;
    private const CardRarity rarity = CardRarity.Common;
    private const TargetType targetType = TargetType.AnyEnemy;
    private const bool shouldShowInCardLibrary = true;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move)];
    public BloodSwap() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }
    public override string PortraitPath => $"res://axeboy/images/cards/blood_swap.png";

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await DamageCmd.Attack(DynamicVars.Damage.IntValue)
        .FromCard(this)
        .Targeting(cardPlay.Target)
        .Execute(choiceContext);
        CardModel card = base.CombatState.CreateCard<Dazed>(base.Owner);
        await CardPileCmd.AddGeneratedCardToCombat(
            card, 
            PileType.Discard, 
            addedByPlayer: true 
        );
    }
    protected override void OnUpgrade()
    {
         DynamicVars.Damage.UpgradeValueBy(3);
    }
}