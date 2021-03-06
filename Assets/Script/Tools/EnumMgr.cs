﻿namespace Assets.Script.Tools
{
    public enum GameStateEnum
    {
        None,
        Intro,
        PlayGame,
        End,
    }

    public enum BinAnimationEnum
    {
        BinClose,
        BinOpen,
        BinRollIn,
    }

    public enum ContainerEnum
    {
        Food,
        Glass,
        Paper,
        Plastic,
        Max,
    }

    public enum EventDefineEnum
    {
       PickUpTrash,
       ReleaseTrash,
       TrashInContainer,
    }

    public enum SoundEnum
    {
        PickUpTrash,
        EndMagicParticle,
        OpenBin,
        CloseBin,
        Trashbinmoving,
        TrashintheBinFood,
        TrashintheBinPlastics,
        TrashintheBinPaper,
        TrashintheBinGlass,
    }

    public enum ColliderStateEnum
    {
        Enter,
        Stay,
        Exit,
    }

    public enum ActorTypeEnum
    {
        MonsterCard,  //野怪卡
        WeaponCard,   //武器卡
        HealCard,     //治疗卡
        ShieldCard,   //盾牌卡
        CoinCard,     //金币
        PlayerEquip,  //角色装备栏
    }

    public enum EquipSpaceTypeEnum
    {
        None,
        LeftEquip,
        RightEquip,
        PlayerPos,
        Package,
    }

    public enum PokerTypeEnum
    {
        Heart,   //红桃
        Spade,   //黑桃
        Diamond, //方块
        Club,    //梅花
    }

    /// <summary>
    /// 配置表名
    /// </summary>

    public enum XmlName
    {
        CardData,  //卡牌属性
     //   AudioData,
    }
}
