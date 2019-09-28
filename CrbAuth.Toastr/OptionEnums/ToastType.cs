using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CrbAuth.Toastr.OptionEnums
{
    public enum ToastType
    {
        [Description("success")]
        Success,

        [Description("info")]
        Info,

        [Description("warning")]
        Warning,

        [Description("error")]
        Error,
    }
}
