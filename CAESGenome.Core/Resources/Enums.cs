using System.ComponentModel;

namespace CAESGenome.Core.Resources
{
    public enum PlateTypes
    {
        [Description("96")]
        NinetySix = 96, 
        [Description("384")]
        ThreeEightyFour = 384, 
        [Description("Q-Tray")]
        QTray, 
        [Description("Glycerol Stock")]
        GlycerolStock
    }
    public enum WellTypes {Standard, Deep}
    public enum SequenceDirection
    {   
        [Description("One")]
        Forward, 
        [Description("Two")]
        Backward
    }

    // Sample types used on the request page for sublibrary
    public enum TypeOfSamples
    {
        [Description("Ecoli wiht BAC")]
        BAC,
        [Description("Purified DNA (10 µg minimum)")]
        DNA
    }

    public enum GriddingPattern
    {
        [Description("3X3")]
        ThreeXThree, 
        [Description("4X4")]
        FourXFour, 
        [Description("5X5")]
        FiveXFive
    };

    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string PI = "PI";
        public const string Staff = "Staff";
        public const string User = "User";
    }
}
