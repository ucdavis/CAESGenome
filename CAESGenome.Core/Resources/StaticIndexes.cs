using System.Collections.Generic;

namespace CAESGenome.Core.Resources
{
    public enum JobTypeIds
    {
        BacterialClone = 1, PCRProduct = 2, UserRunSequencing = 3, PurifiedDna = 4, Sublibrary = 5, UserRuneGenotyping = 11
        , QbotColonyPicking = 21, QbotPlateReplicating = 22, QbotGridding = 23, DnaSubmission = 24
    }

    public static class StageIds
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
        public const string SlWebSubmittedPlates = "SLWP";
        public const string SlPlateSubmission = "SLPS";
        public const string Sl3730xl = "SL37";

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

        public static List<string> WebPlateIds
        {
            get
            {
                return new List<string>() {BcWebSubmittedPlates, PcrWebSubmittedPlates, UrsWebSubmittedPlates, PdWebSubmittedPlates, SlWebSubmittedPlates, UgWebSubmittedPlates, QpWebSubmittedPlates, QrWebSubmittedPlates, QgWebSubmittedPlates};
            }
        }
        public static List<string> LabPlateIds
        {
            get
            {
                return new List<string>() {BcPlateSubmission, PcrPlateSubmission, UrsPlateSubmission, PdPlateSubmission, SlPlateSubmission, UgPlateSubmission, QpPlateSubmission, QrPlateSubmission, QgPlateSubmission};
            }
        }
        public static List<string> RcaPlateIds
        {
            get
            {
                return new List<string>() { BcRca };
            }
        }
        public static List<string> SequencingPlateIds
        {
            get
            {
                return new List<string>() {BcSequencingReaction, PcrSequencingReaction, PdSequencingReaction};
            }
        }
        public static List<string> Xl3730PlateIds
        {
            get
            {
                return new List<string>() {Bc3730xl, Pcr3730xl, Urs3730xl, Pd3730xl, Sl3730xl, Ug3730xl};
            }
        }
    }

    public class EquipmentOperators
    {
        public const string User = "User";
    }
}
