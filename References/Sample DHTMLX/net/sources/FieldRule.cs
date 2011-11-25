using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dhtmlxConnectors
{
    /// <summary>
    /// Represents single WHERE statement item (e.g. "OrderID = 15", StartData > '01-01-2009' etc)
    /// </summary>
    public class FieldRule : Rule
    {
        /// <summary>
        /// Gets or Sets field to compare
        /// </summary>
        public Field Field
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets value to compare field with
        /// </summary>
        public object Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets compare operator 
        /// </summary>
        public Operator Operator
        {
            get;
            set;
        }

        /// <summary>
        /// Creates new instance of FieldRule
        /// </summary>
        /// <param name="RuleField">Field to compare</param>
        /// <param name="CompareOperator">Compare operator</param>
        /// <param name="CompareValue">Value to compare field with</param>
        public FieldRule(Field RuleField, Operator CompareOperator, object CompareValue)
        {
            this.Field = RuleField;
            this.Operator = CompareOperator;
            this.Value = CompareValue;
        }

        /// <summary>
        /// Creates new instance of FieldRule
        /// </summary>
        /// <param name="RuleFieldName">FieldName to compare</param>
        /// <param name="CompareOperator">Compare operator</param>
        /// <param name="CompareValue">Value to compare field with</param>
        public FieldRule(string RuleFieldName, Operator CompareOperator, object CompareValue)
            : this((TableField)RuleFieldName, CompareOperator, CompareValue)
        {
        }


        /// <summary>
        /// Converts this FieldRule into SQL-92 string
        /// </summary>
        /// <returns>String representation of this FieldRule</returns>
        public override string ToString()
        {
            string template = string.Empty;
            if (this.Value == null || this.Field == null)
                return string.Empty;
            if (Convert.ToString(this.Value) == "null")
            {
                if (this.Operator == Operator.Equals)
                    return string.Format("({0} IS NULL)", this.Field.InternalName);
                else
                    return string.Format("(NOT {0} IS NULL)", this.Field.InternalName);
            }

            switch (this.Operator)
            {
                case Operator.Equals:
                    template = "({0} = '{1}')";
                    break;
                case Operator.NotEquals:
                    template = "({0} <> '{1}')";
                    break;
                case Operator.Greater:
                    template = "({0} > '{1}')";
                    break;
                case Operator.GreaterOrEqual:
                    template = "({0} >= '{1}')";
                    break;
                case Operator.Lower:
                    template = "({0} < '{1}')";
                    break;
                case Operator.LowerOrEqual:
                    template = "({0} <= '{1}')";
                    break;
                case Operator.Like:
                    template = "({0} like '{1}')";
                    break;
                case Operator.DoesntLike:
                    template = "NOT ({0} like '{1}')";
                    break;
                default:
                    throw new NotImplementedException(this.Operator.ToString() + " operator support has not been implemented yet!");
            }
            return string.Format(template, this.Field.InternalName, Tools.ConvertToString(this.Value));
        }
    }

    /// <summary>
    /// Supported compare operators
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// =
        /// </summary>
        Equals,
        /// <summary>
        /// !=
        /// </summary>
        NotEquals,
        /// <summary>
        /// >
        /// </summary>
        Greater, 
        /// <summary>
        /// >=
        /// </summary>
        GreaterOrEqual,
        /// <summary>
        /// &lt;
        /// </summary>
        Lower,
        /// <summary>
        /// &lt;=
        /// </summary>
        LowerOrEqual,
        /// <summary>
        /// LIKE '%VALUE%'
        /// </summary>
        Like,
        /// <summary>
        /// NOT LIKE '%VALUE%'
        /// </summary>
        DoesntLike
    }
}
