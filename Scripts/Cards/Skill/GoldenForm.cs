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
public class GoldenForm : CustomCardModel
{
    public override bool GainsBlock => true;
    private const int energyCost = 0;
    protected override bool HasEnergyCostX => true;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Rare;
    public override IEnumerable<CardKeyword> CanonicalKeywords
        => new List<CardKeyword> { CardKeyword.Exhaust};
    private const TargetType targetType = TargetType.Self;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/golden_form.png";

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<SlipperyPower>()
    };
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<VisibilityPower>(4m),
    ];
    
    public GoldenForm() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int powerAmount = ResolveEnergyXValue();
        var visibilityPower = Owner.Creature.GetPower<VisibilityPower>();
        int requiredVisibility = DynamicVars["VisibilityPower"].IntValue;
        if (visibilityPower != null &&visibilityPower.Amount >= requiredVisibility&&visibilityPower != null)
        {
            powerAmount*=2;
            await PowerCmd.Apply<VisibilityPower>(base.Owner.Creature,-DynamicVars["VisibilityPower"].IntValue,base.Owner.Creature,this);
        }
        await PowerCmd.Apply<SlipperyPower>(base.Owner.Creature,powerAmount,base.Owner.Creature,this);
        

    }
    protected override void OnUpgrade()
    {
        base.DynamicVars["VisibilityPower"].UpgradeValueBy(-1);
    }
}
