using AxeBoy.Scripts.Powers;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class FinalCard : CustomCardModel
{
    private const int energyCost = 3;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Rare;
    private const TargetType targetType = TargetType.AllEnemies;
    public override IEnumerable<CardKeyword> CanonicalKeywords
        => new List<CardKeyword> { CardKeyword.Exhaust};
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/final_card.png";
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VisibilityPower>(20m),
    ];

    public FinalCard() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var visibilityPower = Owner.Creature.GetPower<VisibilityPower>();
        int requiredVisibility = DynamicVars["VisibilityPower"].IntValue;
        if (visibilityPower == null || visibilityPower.Amount < requiredVisibility)
        {
            await PowerCmd.Apply<FinalCardPower>(base.Owner.Creature,1, base.Owner.Creature, null);
        }
        foreach (Creature hittableEnemy in base.CombatState.HittableEnemies)
        {
            await CreatureCmd.Kill(hittableEnemy);
        }
    }
    protected override void OnUpgrade()
    {
        if (DynamicVars.TryGetValue("VisibilityPower", out var powerVar))
        {
            powerVar.UpgradeValueBy(-5);
        }
    }
}