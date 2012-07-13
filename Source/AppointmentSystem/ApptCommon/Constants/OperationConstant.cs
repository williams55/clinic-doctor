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

        public static ConstantKeyValue Create = new ConstantKeyValue("C", "Create");
        public static ConstantKeyValue Read = new ConstantKeyValue("R", "Read");
        public static ConstantKeyValue Update = new ConstantKeyValue("U", "Update");
        public static ConstantKeyValue Delete = new ConstantKeyValue("D", "Delete");
    }
}
