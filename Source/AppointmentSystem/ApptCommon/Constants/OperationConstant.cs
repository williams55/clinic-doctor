using Common;

namespace Appt.Common.Constants
{
    /// <summary>
    /// Action of
    /// C: Create
    /// R: Read
    /// U: Update
    /// D: Delete
    /// </summary>
    public class OperationConstant : Constant
    {
        public static OperationConstant Instant()
        {
            return new OperationConstant();
        }

        public class Const
        {
            public const string Create = "C";
            public const string Read = "R";
            public const string Update = "U";
            public const string Delete = "D";
        }

        public static ConstantKeyValue Create = new ConstantKeyValue(Const.Create, "Create");
        public static ConstantKeyValue Read = new ConstantKeyValue(Const.Read, "Read");
        public static ConstantKeyValue Update = new ConstantKeyValue(Const.Update, "Update");
        public static ConstantKeyValue Delete = new ConstantKeyValue(Const.Delete, "Delete");
    }
}
