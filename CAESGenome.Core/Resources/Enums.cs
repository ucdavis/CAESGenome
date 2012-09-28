namespace CAESGenome.Core.Resources
{
    public enum PlateTypes {NinetySix = 96, ThreeEightyFour = 384, QTray, GlycerolStock}
    public enum WellTypes {Standard, Deep}
    public enum SequenceDirection {Forward, Backward}

    // Sample types used on the request page for sublibrary
    public enum TypeOfSamples { BAC, DNA }

    public static class RoleNames
    {
        public const string Admin = "Admin";
        public const string PI = "PI";
        public const string Staff = "Staff";
        public const string User = "User";
    }
}
