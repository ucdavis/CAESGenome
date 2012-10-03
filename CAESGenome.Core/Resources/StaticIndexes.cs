namespace CAESGenome.Core.Resources
{
    public enum JobTypeIds
    {
        BacterialClone = 1, PCRProduct = 2, UserRunSequencing = 3, PurifiedDna = 4, Sublibrary = 5, UserRuneGenotyping = 11
        , QbotColonyPicking = 21, QbotPlateReplicating = 22, QbotGridding = 23, DnaSubmission = 24
    }

    public class StageIds
    {
        // Bacterial Clone
        public const string BcWebSubmittedPlates = "BCWP";
        public const string BcPlateSubmission = "BCPS";
        public const string BcRca = "BCRC";
        public const string BcSequencingReaction = "BCSR";
        public const string Bc3730xl = "BC37";

        // PCR Product
        public const string PcrWebSubmittedPlates = "PPWP";
        public const string PcrPlateSubmission = "PPPS";
        public const string PcrSequencingReaction = "PPSR";
        public const string Pcr3730xl = "PP37";

        // User-Run Sequencing
        public const string UrsWebSubmittedPlates = "URWP";
        public const string UrsPlateSubmission = "URPS";
        public const string Urs3730xl = "UR37";

        // Purified DNA
        public const string PdWebSubmittedPlates = "PDWP";
        public const string PdPlateSubmission = "PDPS";
        public const string PdSequencingReaction = "PDSR";
        public const string Pd3730xl = "PD37";
        
        // Sublibrary
        public const string SlWebSubmittedPlates = "URWP";
        public const string SlPlateSubmission = "URPS";
        public const string Sl3730xl = "UR37";

        // Genotyping
        public const string UgWebSubmittedPlates = "UGWP";
        public const string UgPlateSubmission = "UGPS";
        public const string Ug3730xl = "UG37";

        // Qbot Colony Picking
        public const string QpWebSubmittedPlates = "QPWP";
        public const string QpPlateSubmission = "QPPS";

        // Qbot Plate Replication
        public const string QrWebSubmittedPlates = "QRWP";
        public const string QrPlateSubmission = "QRPS";

        // Qbot Gridding
        public const string QgWebSubmittedPlates = "QGWP";
        public const string QgPlateSubmission = "QGPS";
    }
}
