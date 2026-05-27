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
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.CardSelection;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class SoulWeep : CustomCardModel
{
    private const int energyCost =0;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Rare;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/soul_weep.png";

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<VisibilityPower>(),
        HoverTipFactory.FromPower<ResentfulSoulsPower>(),
    };

    public SoulWeep() : base(energyCost, type, rarity, TargetType.AllEnemies, shouldShowInCardLibrary)
    {
    }
     protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VisibilityPower>(1m),
        new PowerVar<ResentfulSoulsPower>(1m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {   
        int stackCount = base.DynamicVars["ResentfulSoulsPower"].IntValue;
            var ownResentful = base.Owner.Creature.GetPower<DecayRealmAuraPower>();
                    if (ownResentful != null && ownResentful.Amount > 0)
                    {
                        stackCount *= ownResentful.Amount+1;
                    }
        await PowerCmd.Apply<ResentfulSoulsPower>(base.CombatState.HittableEnemies,stackCount,base.Owner.Creature,null);
        await PowerCmd.Apply<VisibilityPower>(base.Owner.Creature,DynamicVars["VisibilityPower"].BaseValue,base.Owner.Creature,null);
        CardModel clone = CreateClone();
        var addAction = await CardPileCmd.AddGeneratedCardToCombat(clone, PileType.Discard, addedByPlayer: true);
        CardCmd.PreviewCardPileAdd(addAction, 1.2f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["VisibilityPower"].UpgradeValueBy(1);
    }
}