using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System.Collections.Generic;
using MegaCrit.Sts2.Core.Models;
using System.Collections;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Rooms;
using AxeBoy.Scripts.Cards;

namespace AxeBoy.Scripts.Powers;

public class FinalCardPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Debuff; // 假设是Debuff，因为减少属性
    public override PowerStackType StackType => PowerStackType.Counter;

    // 自定义图标路径（需要添加图片）
    public override string? CustomPackedIconPath => "res://axeboy/images/powers/final_card_power.png";
    public override string? CustomBigIconPath => "res://axeboy/images/powers/final_card_power.png";
    public override async Task AfterCombatEnd(CombatRoom room)
    {
        // 安全判断：空值/无效层数直接退出
        if (room == null || Owner == null || Owner.Player == null || base.Amount <= 0)
            return;

        List<CardModel> cardsToRemove = base.Owner.Player.Deck.Cards
        .Where(c => c is FinalCard)
        .ToList();


        foreach (CardModel card in cardsToRemove)
        {
            PlayerCmd.CompleteQuest(card);
            await CardPileCmd.RemoveFromDeck(card);
        }
        
        await base.AfterCombatEnd(room);
    }

}