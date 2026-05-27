using AxeBoy.Scripts.Powers;
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
public class DecayStep : CustomCardModel
{
    // 基础耗能
    public override bool GainsBlock => true;

    private const int energyCost = 2;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Uncommon;
    private const TargetType targetType = TargetType.Self;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/decay_step.png";
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(20, ValueProp.Move),new PowerVar<ResentfulSoulsPower>(2m)];
    public DecayStep() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
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
    }
    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(5);
    }
}