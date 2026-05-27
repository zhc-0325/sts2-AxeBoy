using AxeBoy.Scripts.Powers;
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
public class AxeSwing : CustomCardModel
{
    private const int energyCost = 0;
    private const CardType type = CardType.Attack;
    private const CardRarity rarity = CardRarity.Uncommon;
    private const TargetType targetType = TargetType.AnyEnemy;
        protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ResentfulSoulsPower>(2m),
        new DamageVar(11, ValueProp.Move)
        
    ];
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<ResentfulSoulsPower>()
    };
    private const bool shouldShowInCardLibrary = true;
    public AxeSwing() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }
    public override string PortraitPath => $"res://axeboy/images/cards/axe_swing.png";

    // 打出时的效果逻辑
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int stackCount = base.DynamicVars["ResentfulSoulsPower"].IntValue;
            var ownResentful = base.Owner.Creature.GetPower<DecayRealmAuraPower>();
                    if (ownResentful != null && ownResentful.Amount > 0)
                    {
                        stackCount *= ownResentful.Amount+1;
                    }
            await PowerCmd.Apply<ResentfulSoulsPower>(
                base.Owner.Creature,
                stackCount,
                base.Owner.Creature,
                null
            );
        await DamageCmd.Attack(DynamicVars.Damage.IntValue)
        .FromCard(this)
        .Targeting(cardPlay.Target)
        .Execute(choiceContext);
    }
    protected override void OnUpgrade()
    {
         DynamicVars["ResentfulSoulsPower"].UpgradeValueBy(-1);
    }
}