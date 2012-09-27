using Common;

namespace Appt.Common.Constants
{
    public class SexConstant : Constant
    {
        public static SexConstant Instant()
        {
            return new SexConstant();
        }

        public static ConstantKeyValue Female = new ConstantKeyValue("F", "Female");
        public static ConstantKeyValue Male = new ConstantKeyValue("M", "Male");
    }
}
