using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WebCukCuk.Common.Attributes
{
    /// <summary>
    /// Tên thuộc tính
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class PropName : Attribute
    {
        public string propName { get; set; }

        public PropName(string? propName)
        {
            this.propName = propName;
        }

    }

    /// <summary>
    /// Không được bỏ trống
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class NotEmpty : Attribute
    {
    }

    /// <summary>
    /// Validate địa chỉ email
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ValiEmail : Attribute
    {
    }

    /// <summary>
    /// Validate ngày lớn hơn ngày hiện tại
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DateLongerNow : Attribute
    {
    }
}
