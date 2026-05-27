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
public class VenomFang : CustomCardModel
{
    private const int energyCost =2;
    private const CardType type = CardType.Skill;
    private const CardRarity rarity = CardRarity.Uncommon;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/venom_fang.png";
    public override IEnumerable<CardKeyword> CanonicalKeywords
        => new List<CardKeyword> { CardKeyword.Retain};

    protected override IEnumerable<IHoverTip> ExtraHoverTips
    => new List<IHoverTip>
    {
        HoverTipFactory.FromPower<ResentfulSoulsPower>(),
    };

    public VenomFang() : base(energyCost, type, rarity, TargetType.AnyEnemy, shouldShowInCardLibrary)
    {
    }
     protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<ResentfulSoulsPower>(5m)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {   
        await PowerCmd.Apply<ResentfulSoulsPower>(cardPlay.Target,DynamicVars["ResentfulSoulsPower"].BaseValue,base.Owner.Creature,null);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["ResentfulSoulsPower"].UpgradeValueBy(3);
        
    }
}