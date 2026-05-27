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
public class SoulPatrol : CustomCardModel
{
    private const int energyCost = 3;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Rare;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/soul_patrol.png";

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<VisibilityPower>()
    };

    public SoulPatrol() : base(energyCost, type, rarity, TargetType.AnyEnemy, shouldShowInCardLibrary)
    {
    }
     protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VisibilityPower>(3m),
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var visibilityPower = Owner.Creature.GetPower<VisibilityPower>();
        if (visibilityPower != null &&visibilityPower.Amount >= DynamicVars["VisibilityPower"].IntValue)
        {
            await PowerCmd.Apply<VisibilityPower>(base.Owner.Creature,-DynamicVars["VisibilityPower"].IntValue,base.Owner.Creature,this);
        }
        else
        {
            await CreatureCmd.LoseMaxHp(null,base.Owner.Creature, 3, true);
        }
        await PowerCmd.Apply<PerceptionPower>(cardPlay.Target,3,base.Owner.Creature,this);
        await CreatureCmd.Stun(cardPlay.Target);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["VisibilityPower"].UpgradeValueBy(-1);
    }
}