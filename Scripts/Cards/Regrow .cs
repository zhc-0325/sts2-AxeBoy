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
public class Regrow : CustomCardModel
{
    public override bool GainsBlock => true;
    private const int energyCost = 1;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Basic;
    private const TargetType targetType = TargetType.AnyEnemy;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/regrow.png";
    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<ResentfulSoulsPower>()
    };
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ResentfulSoulsPower>(1m)
        
    ];
    
    

    public Regrow() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }


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
                cardPlay.Target,
                stackCount,
                base.Owner.Creature,
                null
            );
        await CreatureCmd.Heal(base.Owner.Creature, 2);
    }
    protected override void OnUpgrade()
    {
        base.EnergyCost.UpgradeBy(-1);
    }
}
