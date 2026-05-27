using AxeBoy.Scripts.Powers;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Monsters;
using MegaCrit.Sts2.Core.ValueProps;

namespace AxeBoy.Scripts.Cards;

[Pool(typeof(AxeBoyCardPool))]
public class GoldenBless : CustomCardModel
{
    public override bool GainsBlock => true;
    private const int energyCost = 1;
    private const CardType type = CardType.Power;
    private const CardRarity rarity = CardRarity.Rare;
    public override IEnumerable<CardKeyword> CanonicalKeywords
        => new List<CardKeyword> { CardKeyword.Exhaust,CardKeyword.Innate };
    private const TargetType targetType = TargetType.Self;
    private const bool shouldShowInCardLibrary = true;
    public override string PortraitPath => $"res://axeboy/images/cards/golden_bless.png";
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<DecayRealmAuraPower>(3m)
    ];

    public GoldenBless() : base(energyCost, type, rarity, targetType, shouldShowInCardLibrary)
    {
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await PlayerCmd.GainGold(100, base.Owner);
        for(int i=0;i<DynamicVars["DecayRealmAuraPower"].IntValue;i++){
        string slot = $"EnemySlot{i}";

        Creature monster = await CreatureCmd.Add<GasBomb>(base.CombatState, null);

        monster.PrepareForNextTurn(
            base.CombatState.Players.Select(p => p.Creature),
            rollNewMove: true
        );
        }
    }
    protected override void OnUpgrade()
    {
        DynamicVars["DecayRealmAuraPower"].UpgradeValueBy(-1);
    }
}
