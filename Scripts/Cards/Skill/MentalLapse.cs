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

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class MentalLapse : CustomCardModel
{
    private const int energyCost = 2;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Rare;
    public override IEnumerable<CardKeyword> CanonicalKeywords
        => new List<CardKeyword> { CardKeyword.Exhaust};
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/mental_lapse.png";

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<MentalLapsePower>()
    };

    public MentalLapse() : base(energyCost, type, rarity, TargetType.Self, shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int hp = Owner.Creature.CurrentHp;
        var visibilityPower = Owner.Creature.GetPower<VisibilityPower>();
        if (visibilityPower != null &&visibilityPower.Amount >= 5)
        {
            hp+=10;
            await PowerCmd.Apply<VisibilityPower>(base.Owner.Creature,-5,base.Owner.Creature,this);
        }
        await PowerCmd.SetAmount<MentalLapsePower>(
            Owner.Creature,
            hp,
            Owner.Creature,
            null
        );
    }

    protected override void OnUpgrade()
    {
        CardCmd.ApplyKeyword(this, CardKeyword.Retain);
    }
}